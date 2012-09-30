using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using oAuthConnection;
using System.Net;
using Tweetinvi.Utils;

namespace Tweetinvi.Utils
{
    // TODO Implement ICloneable
    public class User : TwitterObject
    {
        #region Private Attributes

        #region Twitter API Attributes
        private bool? _is_translator;
        // Implement notifications
        private object _notifications;
        private bool? _profile_use_background_image;
        private string _profile_background_image_url_https;
        private string _time_zone;
        private string _profile_text_color;
        private string _profile_image_url_https;
        // Implement following
        private object[] _following;
        private bool? _verified;
        private string _profile_background_image_url;
        private bool? _default_profile_image;
        private string _profile_link_color;
        private string _description;
        private string _id_str;
        private bool? _contributors_enabled;
        private bool? _geo_enabled;
        private int? _favourites_count;
        private int? _followers_count;
        private string _profile_image_url;
        // Implement private object _follow_request_sent;
        private DateTime _created_at;
        private string _profile_background_color;
        private bool? _profile_background_tile;
        private int? _friends_count;
        private string _url;
        private bool? _show_all_inline_media;
        private int? _statuses_count;
        private string _profile_sidebar_fill_color;
        private bool? _protected;
        private string _screen_name;
        private int? _listed_count;
        private string _name;
        private string _profile_sidebar_border_color;
        private string _location;
        private long? _id;
        private bool? _default_profile;
        private string _lang;
        private int? _utc_offset;
        #endregion

        #region User API Attributes
        private Token _token;
        private List<long> _friend_ids = new List<long>();
        private List<User> _friends;
        private List<long> _follower_ids;
        private List<User> _followers;
        private List<User> _contributors;
        private List<User> _contributees;
        #endregion

        #endregion

        #region Public Attributes

        public Token token
        {
            get { return _token; }
            set { _token = value; }
        }

        #region Twitter API Attributes
        // This region represents the information accessible from a Twitter API
        // when querying for a User

        public bool? is_translator
        {
            get { return _is_translator; }
            set { _is_translator = value; }
        }

        // Implement notifications
        public object notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }
        public bool? profile_use_background_image
        {
            get { return _profile_use_background_image; }
            set { _profile_use_background_image = value; }
        }
        public string profile_background_image_url_https
        {
            get { return _profile_background_image_url_https; }
            set { _profile_background_image_url_https = value; }
        }
        public string time_zone
        {
            get { return _time_zone; }
            set { _time_zone = value; }
        }

        public string profile_text_color
        {
            get { return _profile_text_color; }
            set { _profile_text_color = value; }
        }

        public string profile_image_url_https
        {
            get { return _profile_image_url_https; }
            set { _profile_image_url_https = value; }
        }

        // Implement following
        public object[] following
        {
            get { return _following; }
            set { _following = value; }
        }

        public bool? verified
        {
            get { return _verified; }
            set { _verified = value; }
        }

        public string profile_background_image_url
        {
            get { return _profile_background_image_url; }
            set { _profile_background_image_url = value; }
        }

        public bool? default_profile_image
        {
            get { return _default_profile_image; }
            set { _default_profile_image = value; }
        }

        public string profile_link_color
        {
            get { return _profile_link_color; }
            set { _profile_link_color = value; }
        }

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string id_str
        {
            get { return _id_str; }
            set { _id_str = value; }
        }
        public bool? contributors_enabled
        {
            get { return _contributors_enabled; }
            set { _contributors_enabled = value; }
        }
        public bool? geo_enabled
        {
            get { return _geo_enabled; }
            set { _geo_enabled = value; }
        }
        public int? favourites_count
        {
            get { return _favourites_count; }
            set { _favourites_count = value; }
        }
        public int? followers_count
        {
            get { return _followers_count; }
            set { _followers_count = value; }
        }
        public string profile_image_url
        {
            get { return _profile_image_url; }
            set { _profile_image_url = value; }
        }
        //public object follow_request_sent
        //{
        //    get { return _follow_request_sent; }
        //    set { _follow_request_sent = value; }
        //}
        public DateTime created_at
        {
            get { return _created_at; }
            set { _created_at = value; }
        }
        public string profile_background_color
        {
            get { return _profile_background_color; }
            set { _profile_background_color = value; }
        }
        public bool? profile_background_tile
        {
            get { return _profile_background_tile; }
            set { _profile_background_tile = value; }
        }
        public int? friends_count
        {
            get { return _friends_count; }
            set { _friends_count = value; }
        }
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        public bool? show_all_inline_media
        {
            get { return _show_all_inline_media; }
            set { _show_all_inline_media = value; }
        }
        public int? statuses_count
        {
            get { return _statuses_count; }
            set { _statuses_count = value; }
        }
        public string profile_sidebar_fill_color
        {
            get { return _profile_sidebar_fill_color; }
            set { _profile_sidebar_fill_color = value; }
        }
        public bool? user_protected
        {
            get { return _protected; }
            set { _protected = value; }
        }
        public string screen_name
        {
            get { return _screen_name; }
            set { _screen_name = value; }
        }
        public int? listed_count
        {
            get { return _listed_count; }
            set { _listed_count = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string profile_sidebar_border_color
        {
            get { return _profile_sidebar_border_color; }
            set { _profile_sidebar_border_color = value; }
        }
        public string location
        {
            get { return _location; }
            set { _location = value; }
        }
        public long? id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool? default_profile
        {
            get { return _default_profile; }
            set { _default_profile = value; }
        }

        public string lang
        {
            get { return _lang; }
            set { _lang = value; }
        }

        public int? utc_offset
        {
            get { return _utc_offset; }
            set { _utc_offset = value; }
        }
        #endregion

        #region User API Attributes
        // Friends
        public List<long> Friend_ids
        {
            get { return _friend_ids; }
            set { _friend_ids = value; }
        }

        public List<User> Friends
        {
            get { return _friends; }
            set { _friends = value; }
        }

        // Followers
        public List<long> Follower_ids
        {
            get { return _follower_ids; }
            set { _follower_ids = value; }
        }

        public List<User> Followers
        {
            get { return _followers; }
            set { _followers = value; }
        }

        public List<User> contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        public List<User> contributees
        {
            get { return _contributees; }
            set { _contributees = value; }
        }

        #endregion

        #endregion

        #region User API Queries

        public string query_user_from_id = "https://api.twitter.com/1/users/lookup.json?user_id={0}";
        public string query_user_from_name = "https://api.twitter.com/1/users/lookup.json?screen_name={0}";
        public string query_user_friends = "https://api.twitter.com/1/friends/ids.json?user_id={0}";
        public string query_user_friends_from_name = "https://api.twitter.com/1/friends/ids.json?screen_name={0}";
        public string query_user_followers = "https://api.twitter.com/1/followers/ids.json?user_id={0}";
        public string query_user_followers_from_name = "https://api.twitter.com/1/followers/ids.json?screen_name={0}";
        public string query_user_profile_image = "https://api.twitter.com/1/users/profile_image?user_id={0}&size={1}";
        public string query_user_profile_image_from_name = "https://api.twitter.com/1/users/profile_image?screen_name={0}&size={1}";
        public string query_user_contributors_from_id = "https://api.twitter.com/1/users/contributors.json?user_id={0}";
        public string query_user_contributors_from_name = "https://api.twitter.com/1/users/contributors.json?screen_name={0}";
        public string query_user_contributees_from_id = "https://api.twitter.com/1/users/contributees.json?user_id={0}";
        public string query_user_contributees_from_name = "https://api.twitter.com/1/users/contributees.json?screen_name={0}";

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor Enabling DataGrid new Elements
        /// </summary>
        private User()
        {
        }

        #region Create User from Id
        /// <summary>
        /// Create a Tweet and retrieve the propreties through given token
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="token">Token saved in class propreties</param>
        public User(long? id, Token token = null)
            : this()
        {
            if (id == null)
                throw new Exception("id cannot be null!");
            _id = id;
            _id_str = id.ToString();

            if (token != null)
            {
                this.token = token;
                this.FillUser(token);
            }
        }

        public User(long id, Token token = null)
            : this((long?)id, token) { }

        #endregion

        #region Create User from username
        /// <summary>
        /// Create a Tweet and retrieve the propreties through given token
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="token">Token saved in class propreties</param>
        public User(string username, Token token = null)
            : this()
        {
            _screen_name = username;
            if (token != null)
            {
                this.token = token;
                this.FillUser(token);
            }
        }

        #endregion

        #region Create User from dynamic response
        private User(dynamic dynamicUser)
        {
            if (dynamicUser is Dictionary<String, object>)
            {
                Dictionary<String, object> dUser = dynamicUser as Dictionary<String, object>;
                fillUser(dUser);
            }
        }

        public static User Create(object dynamicUser)
        {
            if (dynamicUser is Dictionary<String, object>)
            {
                try
                {
                    return new User(dynamicUser);
                }
                catch (InvalidOperationException) { return null; }
            }

            return null;
        }
        #endregion
        #endregion

        #region Private Methods
        #region FillUser
        /// <summary>
        /// Filling all the information related with a user
        /// </summary>
        /// <param name="dUser">Dictionary containing all the information coming from Twitter</param>
        private void fillUser(Dictionary<String, object> dUser)
        {
            if (dUser.GetProp("id") != null || dUser.GetProp("screen_name") != null)
            {
                is_translator = dUser.GetProp("is_translator") as bool?;
                notifications = dUser.GetProp("notifications");
                profile_use_background_image = dUser.GetProp("profile_use_background_image") as bool?;
                profile_background_image_url_https = dUser.GetProp("profile_background_image_url_https") as string;
                time_zone = dUser.GetProp("time_zone") as string;
                profile_text_color = dUser.GetProp("profile_text_color") as string;
                profile_image_url_https = dUser.GetProp("profile_image_url_https") as string;
                following = dUser.GetProp("following") as object[];
                verified = dUser.GetProp("verified") as bool?;
                profile_background_image_url = dUser.GetProp("profile_background_image_url") as string;
                default_profile_image = dUser.GetProp("default_profile_image") as bool?;
                profile_link_color = dUser.GetProp("profile_link_color") as string;
                description = dUser.GetProp("description") as string;
                id_str = dUser.GetProp("id_str") as string;
                contributors_enabled = dUser.GetProp("contributors_enabled") as bool?;
                geo_enabled = dUser.GetProp("geo_enabled") as bool?;
                favourites_count = dUser.GetProp("favourites_count") as int?;
                followers_count = dUser.GetProp("followers_count") as int?;
                profile_image_url = dUser.GetProp("profile_image_url") as string;
                //follow_request_sent = dUser.GetProp("follow_request_sent") as ;
                created_at = DateTime.ParseExact(dUser.GetProp("created_at") as string,
                    "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
                profile_background_color = dUser.GetProp("profile_background_color") as string;
                profile_background_tile = dUser.GetProp("profile_background_tile") as bool?;
                friends_count = dUser.GetProp("friends_count") as int?;
                url = dUser.GetProp("url") as string;
                show_all_inline_media = dUser.GetProp("show_all_inline_media") as bool?;
                statuses_count = dUser.GetProp("statuses_count") as int?;
                profile_sidebar_fill_color = dUser.GetProp("profile_sidebar_fill_color") as string;
                user_protected = dUser.GetProp("protected") as bool?;
                screen_name = dUser.GetProp("screen_name") as string;
                listed_count = dUser.GetProp("listed_count") as int?;
                name = dUser.GetProp("name") as string;
                profile_sidebar_border_color = dUser.GetProp("profile_sidebar_border_color") as string;
                location = dUser.GetProp("location") as string;
                var salut = dUser.GetProp("id");
                id = Convert.ToInt64(dUser.GetProp("id")) as long?;
                default_profile = dUser.GetProp("default_profile") as bool?;
                lang = dUser.GetProp("lang") as string;
                utc_offset = dUser.GetProp("utc_offset") as int?;
            }
            else
                throw new InvalidOperationException("Cannot create 'User' if id does not exist");
        } 
        #endregion

        #region Get Contributors
        /// <summary>
        /// Update the exception handler attribute with the 3rd parameter
        /// Get the list of users matching the Twitter request url (contributors or contributees)
        /// </summary>
        /// <param name="token"> Current user token to access the Twitter API</param>
        /// <param name="url">Twitter requets URL</param>
        /// <param name="exceptionHandlerDelegate">Delegate method to handle Twitter request exceptions</param>
        /// <returns>Null if the token parameter is null or if the Twitter request fails. A list of users otherwise (contributors or contributees).</returns>
        private List<User> getContributionObjects(Token token, String url, Token.WebExceptionHandlingDelegate exceptionHandlerDelegate = null)
        {
            //
            if (token == null)
            {
                Console.WriteLine("User's token is needed");
                return null;
            }

            // Update the exception handler
            //token.Integrated_Exception_Handler = false;
            token.ExceptionHandler = exceptionHandlerDelegate;


            dynamic webRequestResult = token.ExecuteQuery(url);

            List<User> result = null;
            if (webRequestResult != null)
            {
                // Create and fill a user list with the data retrieved from Twitter (Dictionary<String, object>[])
                result = new User[] { }.ToList();
                if (webRequestResult is object[])
                {
                    object[] retrievedContributors = webRequestResult as object[];
                    foreach (object rc in retrievedContributors)
                    {
                        if (rc is Dictionary<String, object>)
                        {
                            Dictionary<String, object> userInfo = rc as Dictionary<String, object>;
                            User u = new User();
                            u.fillUser(userInfo);
                            result.Add(u);
                        }
                    }
                }
            }

            return result;
        } 
        #endregion
        #endregion

        #region Public Methods

        #region FillUser

        /// <summary>
        /// Fill User basic information retrieving the information thanks to the
        /// default Token
        /// </summary>
        public void FillUser()
        {
            FillUser(this.token);
        }

        /// <summary>
        /// Fill User basic information retrieving the information thanks to a Token
        /// <param name="token">Token to use to get infos</param>
        public void FillUser(Token token)
        {
            if (token != null)
            {
                var query = "";
                if (_id != null)
                    query = String.Format(this.query_user_from_id, id);
                else
                    if (_screen_name != null)
                        query = String.Format(this.query_user_from_name, screen_name);
                    else
                        return;

                var jsonResponse = token.ExecuteQuery(query);
                dynamic dynamicUser = jsonResponse[0];
                if (dynamicUser is Dictionary<String, object>)
                {
                    Dictionary<String, object> dUser = dynamicUser as Dictionary<String, object>;
                    fillUser(dUser);
                }
            }
        }
        #endregion

        #region Get Friends
        public List<long> getFriends(bool createUserList = false, long cursor = 0)
        {
            return getFriends(this.token, createUserList, cursor);
        }

        public List<long> getFriends(Token token, bool createUserList = false, long cursor = 0)
        {
            if (token == null)
                return null;

            if (cursor == 0)
            {
                Friend_ids = new List<long>();
                Friends = new List<User>();
            }

            Token.DynamicResponseDelegate del = delegate(dynamic responseObject, long previous_cursor, long next_cursor)
            {
                foreach (var friend_id in responseObject["ids"])
                {
                    Friend_ids.Add((long)friend_id);
                    if (createUserList)
                        Friends.Add(new User((long)friend_id));
                }
            };

            if (id != null)
                token.ExecuteCursorQuery(String.Format(query_user_friends, id), del);
            else
                if (_screen_name != null)
                    token.ExecuteCursorQuery(String.Format(query_user_friends_from_name, screen_name), del);

            return Friend_ids;
        }

        #endregion

        #region Get Followers

        public List<long> getFollowers(bool createFollowerList = false, long cursor = 0)
        {
            return getFollowers(this.token, createFollowerList, cursor);
        }

        public List<long> getFollowers(Token token, bool createFollowerList = false, long cursor = 0)
        {
            if (token == null)
                return null;

            if (cursor == 0)
            {
                Follower_ids = new List<long>();
                Followers = new List<User>();
            }

            Token.DynamicResponseDelegate del = delegate(dynamic responseObject, long previous_cursor, long next_cursor)
            {
                foreach (var follower_id in responseObject["ids"])
                {
                    Follower_ids.Add((long)follower_id);
                    if (createFollowerList)
                        Followers.Add(new User((long)follower_id));
                }
            };

            if (id != null)
                token.ExecuteCursorQuery(String.Format(query_user_followers, id), del);
            else
                if (_screen_name != null)
                    token.ExecuteCursorQuery(String.Format(query_user_followers_from_name, screen_name), del);

            return Follower_ids;
        }

        #endregion

        #region Get Profile Image

        public string GetProfileImage(ImageSize size = ImageSize.normal, bool download = false, string location = "")
        {
            return GetProfileImage(token, size, download, location);
        }

        /// <summary>
        /// Get the Profile Image for a user / Possibility to download it
        /// </summary>
        /// <param name="token"></param>
        /// <param name="size">Size of the image</param>
        /// <param name="download">Define if you want to download the image</param>
        /// <param name="location">Define location to store it</param>
        /// <returns>Url to access the image from a browser</returns>
        public string GetProfileImage(Token token, ImageSize size = ImageSize.normal, bool download = false, string location = "")
        {
            if (token == null)
                return null;

            if (screen_name == null && id == null)
                return null;

            string url = null;
            string img_name = null;

            if (screen_name != null)
            {
                url = String.Format(query_user_profile_image_from_name, screen_name, size);
                img_name = screen_name;
            }
            else
                if (id != null)
                {
                    url = String.Format(query_user_profile_image, id, size);
                    img_name = id.ToString();
                }


            // Using WebClient
            if (download && url != null)
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, String.Format("{0}{1}_{2}.jpg", location, img_name, size));
                }

            #region Note
            // Using WebRequest
            // if you want to change from WebClient to WebRequest you can simply use the code behind by extending the class
            // WebRequest request = WebRequest.Create(String.Format(query_user_profile_image, screen_name, size));
            // WebResponse response = request.GetResponse(); 
            #endregion

            return url;
        }

        #endregion

        #region Get Contributors
        /// <summary>
        /// Get the list of contributors to the account of the current user
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributors
        /// </summary>
        /// <param name="createContributorList">False by default. Indicates if the _contributors attribute needs to be updated with the result</param>
        /// <returns>The list of contributors to the account of the current user</returns>
        public List<User> GetContributors(bool createContributorList = false)
        {
            // Specific error handler
            // Manage the error 400 thrown when contributors are not enabled by the current user
            Token.WebExceptionHandlingDelegate del = delegate(WebException wex)
            {
                int indexOfStatus = wex.Response.Headers.AllKeys.ToList().IndexOf("Status");
                string statusValue = wex.Response.Headers.Get(indexOfStatus);
                char[] t = new char[] { ' ' };
                string[] statusContent = statusValue.Split(t);
                if (statusContent != null && statusContent.Length > 0)
                {
                    switch (statusContent[0])
                    {
                        case "400":
                            // Don't need to do anything, the method will return null
                            Console.WriteLine("Contributors are not enabled for this user");
                            break;
                        default:
                            // Other errors are not managed
                            throw wex;
                    }
                }
                else
                {
                    throw wex;
                }
            };

            List<User> result = null;
            // Contributors can be researched according to the user's id or screen_name
            if (this.id != null)
            {
                result = getContributionObjects(this.token, String.Format(this.query_user_contributors_from_id, this.id), del);
            }
            else if (this.screen_name != null)
            {
                result = getContributionObjects(this.token, String.Format(this.query_user_contributors_from_name, this.screen_name), del);
            }

            // Update the attribute _contributors if required
            if (createContributorList)
            {
                _contributors = result;
            }
            return result;
        }

        /// <summary>
        /// Get the list of accounts the current user is allowed to update
        /// Update the matching attribute of the current user if the parameter is true
        /// Return the list of contributees
        /// </summary>
        /// <param name="createContributeeList">False by default. Indicates if the _contributees attribute needs to be updated with the result</param>
        /// <returns>The list of accounts the current user is allowed to update</returns>
        public List<User> GetContributees(bool createContributeeList = false)
        {
            List<User> result = null;
            // Contributees can be researched according to the user's id or screen_name
            if (this.id != null)
            {
                result = getContributionObjects(this.token, String.Format(this.query_user_contributees_from_id, this.id));
            }
            else if (this.screen_name != null)
            {
                result = getContributionObjects(this.token, String.Format(this.query_user_contributees_from_name, this.screen_name));
            }
            // Update the _contributees attribute if needed
            if (createContributeeList)
            {
                _contributees = result;
            }
            return result;
        }
        #endregion

        #endregion
    }
}
