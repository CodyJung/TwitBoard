using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Net;
using oAuthConnection.Utils;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;

namespace oAuthConnection
{
    public partial class Token : INotifyPropertyChanged
    {
        #region Delegates

        private delegate string GenerateConnectionDelegate(SortedDictionary<string, string> parameters, Uri method_uri);
        public delegate void WebExceptionHandlingDelegate(WebException ex);

        #endregion

        #region Private Attributes

        /// <summary>
        /// This delegate will be called when an error occurs
        /// </summary>
        private WebExceptionHandlingDelegate _exceptionHandler;

        /// <summary>
        /// This delegate has been implemented to authorize different way to create the WebRequest
        /// </summary>
        private GenerateConnectionDelegate _generateDelegate;

        /// <summary>
        /// Jss is an object allowing to decrypt information from Json
        /// instanciation is only done 1 time to improve performances.
        /// </summary>
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// Struct of methods to query the Twitter API
        /// </summary>
        private HttpMethod _method { get; set; }

        /// <summary>
        /// Parameters passed to the URL query
        /// </summary>
        private SortedDictionary<string, string> _queryParameters { get; set; }

        /// <summary>
        /// Define if the error are automatically handled
        /// </summary>
        private bool _integrated_exception_handler = false;

        /// <summary>
        /// RateLimit of a Token - Retrieved only when calling GetRateLimit()
        /// </summary>
        private int _xRateLimit;

        /// <summary>
        /// RateLimit Remaining for the Token
        /// </summary>
        private int _xRateLimitRemaining;

        /// <summary>
        /// ResetTime before RateLimit change to be 0
        /// </summary>
        private int _resetTimeInSeconds;

        #endregion

        #region Public Attributes

        #region Exception Handler
        public bool Integrated_Exception_Handler
        {
            get { return _integrated_exception_handler; }
            set
            {
                _integrated_exception_handler = value;
                if (value == true)
                {
                    _exceptionHandler = exceptionHandling;
                }
            }
        }

        public WebExceptionHandlingDelegate ExceptionHandler
        {
            get { return _exceptionHandler; }
            set
            {
                _exceptionHandler = value;

                if (_exceptionHandler != null)
                    _integrated_exception_handler = false;
            }
        }

        public int XRateLimit
        {
            get { return _xRateLimit; }
            set { _xRateLimit = value; }
        }

        public int XRateLimitRemaining
        {
            get { return _xRateLimitRemaining; }
            set
            {
                _xRateLimitRemaining = value;
                OnPropertyChanged("XRateLimitRemaining");
            }
        }

        public int ResetTimeInSeconds
        {
            get { return _resetTimeInSeconds; }
            set { _resetTimeInSeconds = value; }
        }

        #endregion

        #endregion

        #region User API queries
        private string query_rate_limit = "https://api.twitter.com/1/account/rate_limit_status.json";
        #endregion

        #region Private Methods

        /// <summary>
        /// Method Allowing to initialize a SortedDictionnary to enable oAuth query to be generated with
        /// these parameters
        /// </summary>
        /// <param name="method_uri">This is the Uri that will be required for creating the oAuth WebRequest</param>
        /// <returns>Call the method defined in the _generateDelegate and return a string result
        /// This result will be the header of the WebRequest.</returns>
        private string generateParameters(Uri method_uri)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string oauth_timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();

            string oauth_nonce = new Random().Next(123400, 9999999).ToString();

            SortedDictionary<String, String> parameters = new SortedDictionary<string, string>();
            parameters.Add("oauth_version", "1.0");
            parameters.Add("oauth_nonce", oauth_nonce);
            parameters.Add("oauth_timestamp", oauth_timestamp);
            parameters.Add("oauth_signature_method", "HMAC-SHA1");
            parameters.Add("oauth_consumer_key", StringFormater.UrlEncode(this.ConsumerKey));
            parameters.Add("oauth_token", StringFormater.UrlEncode(this.AccessToken));
            parameters.Add("oauth_consumer_secret", StringFormater.UrlEncode(this.ConsumerSecret));
            parameters.Add("oauth_token_secret", StringFormater.UrlEncode(this.AccessTokenSecret));

            return _generateDelegate != null ? _generateDelegate(parameters, method_uri) : null;
        }

        /// <summary>
        /// Header creation, please notice that the headers must be done in a Sorted order
        /// </summary>
        /// <param name="parameters">Parameters of the query</param>
        /// <param name="method_uri">Uri to be called</param>
        /// <returns>Header of the WebRequest</returns>
        private String generateHeaders(SortedDictionary<String, String> parameters, Uri method_uri)
        {
            String[] secretParameters = new[] { "oauth_consumer_key", "oauth_signature_method", 
                "oauth_timestamp", "oauth_nonce", "oauth_version", "oauth_token" };
            //StringBuilder queryParameters = new StringBuilder();
            parameters.Add("oauth_signature", generateSignature(parameters, method_uri));

            StringBuilder header = new StringBuilder("OAuth ");
            foreach (var param in (from p in parameters orderby p.Key where (secretParameters.Contains(p.Key)) == true select p))
            {
                if (header.Length > 6)
                    header.Append(",");
                header.Append(string.Format("{0}=\"{1}\"", param.Key, param.Value));
            }

            header.AppendFormat(",oauth_signature=\"{0}\"", parameters["oauth_signature"]);

            return header.ToString();
        }

        /// <summary>
        /// Encryption of the data to be sent
        /// </summary>
        /// <param name="parameters">Parameters of the query</param>
        /// <param name="method_uri">Uri to be called</param>
        /// <returns>Header of the WebRequest</returns>
        private string generateSignature(SortedDictionary<String, String> parameters, Uri method_uri)
        {
            String[] secretParameters = new[] { "oauth_consumer_key", "oauth_nonce", 
                "oauth_signature_method", "oauth_timestamp", "oauth_token", "oauth_version" };

            List<KeyValuePair<String, String>> orderedParams = new List<KeyValuePair<string, string>>();
            foreach (var query_param in _queryParameters)
                orderedParams.Add(query_param);

            foreach (var param in (from p in parameters orderby p.Key where (secretParameters.Contains(p.Key)) == true select p))
                orderedParams.Add(param);

            StringBuilder queryParameters = new StringBuilder();

            foreach (var param in (from p in orderedParams orderby p.Key select p))
            {
                if (queryParameters.Length > 0)
                    queryParameters.Append("&");
                queryParameters.Append(string.Format("{0}={1}", param.Key, param.Value));
            }

            string url = method_uri.Query == "" ? method_uri.AbsoluteUri : method_uri.AbsoluteUri.Replace(method_uri.Query, "");
            string oAuthRequest = string.Format("{0}&{1}&{2}", _method.ToString(),
                StringFormater.UrlEncode(url), StringFormater.UrlEncode(queryParameters.ToString()));

            string oAuthSecretkey = string.Format("{0}&{1}",
                StringFormater.UrlEncode(this.ConsumerSecret), StringFormater.UrlEncode(this.AccessTokenSecret));
            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(oAuthSecretkey));

            return StringFormater.UrlEncode(Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(oAuthRequest))));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get Information concerning the Token querying limitations
        /// </summary>
        /// <returns>Number of queries available</returns>
        public int GetRateLimit()
        {
            Dictionary<String, object> rate_infos = (Dictionary<String, object>) ExecuteQuery(query_rate_limit);
            XRateLimit = Int32.Parse(rate_infos["hourly_limit"].ToString());
            ResetTimeInSeconds = Int32.Parse(rate_infos["reset_time_in_seconds"].ToString());
            return XRateLimitRemaining;
        }

        /// <summary>
        /// Generate a HttpWebResponse to enable Twitter connection
        /// </summary>
        /// <param name="method_uri">
        ///     The Uri represents the Url to be requested on Twitter API
        /// </param>
        /// <returns>Return a web_request to be used thanks to a webresponse and query twitter</returns>
        public HttpWebRequest GenerateRequest(Uri method_uri)
        {
            _generateDelegate = new GenerateConnectionDelegate(generateHeaders);
            #region Initializing query parameters
            _queryParameters = new SortedDictionary<string, string>();

            if (!string.IsNullOrEmpty(method_uri.Query))
            {
                foreach (Match variable in Regex.Matches(method_uri.Query, @"(?<varName>[^&?=]+)=(?<value>[^&?=]+)"))
                    _queryParameters.Add(variable.Groups["varName"].Value, variable.Groups["value"].Value);
            }

            #endregion

            #region Creating the webRequest
            string query = method_uri.ToString();
            string headersParameters = generateParameters(method_uri);

            try
            {
                HttpWebRequest webRequest = WebRequest.Create(method_uri.ToString()) as HttpWebRequest;
                webRequest.Method = this._method.ToString();
                webRequest.Headers.Add("Authorization", headersParameters);
                webRequest.ServicePoint.Expect100Continue = false;
                return webRequest;
            }
            catch (WebException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }

        /// <summary>
        /// Method allowing to query and url
        /// </summary>
        /// <param name="method_url">Url from Twitter Api to query</param>
        /// <returns>
        /// Return a dynamic object containing the json information 
        /// representing the value from a json response
        /// </returns>
        public dynamic ExecuteQuery(string method_url, WebExceptionHandlingDelegate exceptionHandlerDelegate = null)
        {
            dynamic result = null;
            try
            {
                HttpWebRequest web_request = GenerateRequest(new Uri(method_url));
                WebResponse response = web_request.GetResponse();
                StreamReader responseReader = new StreamReader(web_request.GetResponse().GetResponseStream());

                if (response.Headers["X-RateLimit-Remaining"] != null)
                    XRateLimitRemaining = Int32.Parse(response.Headers["X-RateLimit-Remaining"]);

                string jsonText = responseReader.ReadLine();
                result = jss.Deserialize<dynamic>(jsonText);

                response.Close();
                web_request.Abort();

            }
            catch (WebException wex)
            {
                if (exceptionHandlerDelegate != null)
                    exceptionHandlerDelegate(wex);
                else
                    if (ExceptionHandler != null)
                        ExceptionHandler(wex);
                    else
                        throw wex;
            }

            return result;
        }

        #region Execute Cursor Query
        public delegate void DynamicResponseDelegate(dynamic responseObject, long previous_cursor, long next_cursor);
        public dynamic ExecuteCursorQuery(string method_url, DynamicResponseDelegate cursor_delegate)
        {
            return ExecuteCursorQuery(method_url, 0, cursor_delegate);
        }

        public dynamic ExecuteCursorQuery(string method_url, long cursor = 0,
            DynamicResponseDelegate cursor_delegate = null, WebExceptionHandlingDelegate exceptionHandlerDelegate = null)
        {
            long previous_cursor = cursor;
            long next_cursor = -1;

            while (previous_cursor != next_cursor)
            {
                dynamic responseObject = null;
                try
                {
                    responseObject = this.ExecuteQuery(String.Format("{0}&cursor={1}", method_url, next_cursor));

                    if (responseObject != null)
                    {
                        previous_cursor = (long)responseObject["previous_cursor"];
                        next_cursor = (long)responseObject["next_cursor"];
                    }

                    if (cursor_delegate != null)
                        cursor_delegate(responseObject, previous_cursor, next_cursor);
                }
                catch (WebException wex)
                {
                    if (exceptionHandlerDelegate != null)
                        exceptionHandlerDelegate(wex);
                    else
                        if (ExceptionHandler != null)
                            ExceptionHandler(wex);
                        else
                            throw wex;
                }
            }

            return null;
        }
        #endregion

        #region Optional Exception Handler
        private void exceptionHandling(WebException wex)
        {
            int indexOfStatus = wex.Response.Headers.AllKeys.ToList().IndexOf("Status");
            string statusValue = wex.Response.Headers.Get(indexOfStatus);
            switch (statusValue)
            {
                case "400 Bad Request":
                    if (Int32.Parse(wex.Response.Headers["X-RateLimit-Remaining"]) == 0)
                    {
                        DateTime actualTime = DateTime.Now;
                        DateTime resetTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(Int32.Parse(wex.Response.Headers["X-RateLimit-Reset"]));
                        int sleepDuration = (int)resetTime.Subtract(actualTime).TotalSeconds;
                        Console.WriteLine(String.Format("Token will be reset in {0} seconds...", sleepDuration));
                        Thread.Sleep(sleepDuration);
                    }
                    else
                        throw wex;
                    break;
                case "401 Unauthorized":
                    // The query cannot be performed with the actual Token
                    throw wex;
                case "404 Not Found":
                    // No result has been returned
                    throw wex;
                case "500 Internal Server Error":
                case "503 Server Unavailable":
                    // Error occured on Twitter Webservers
                    break;
                default:
                    throw wex;
            }
        }
        #endregion
        #endregion
    }
}
