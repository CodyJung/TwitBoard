using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using oAuthConnection;
using System.Configuration;
using Tweetinvi;
using Tweetinvi.Utils;
using System.Net;

namespace Examplinvi
{
    class Program
    {
        #region Run
        #region Token

        #region Execute Query
        /// <summary>
        /// Simple function that uses ExecuteQuery to retrieve information from the Twitter API
        /// </summary>
        /// <param name="token"></param>
        static void ExecuteQuery(Token token)
        {
            // Retrieving information from Twitter API through Token method ExecuteRequest
            dynamic timeline = token.ExecuteQuery("https://api.twitter.com/1/statuses/home_timeline.json");

            // Working on each different object sent as a response to the Twitter API query
            for (int i = 0; i < timeline.Length; ++i)
            {
                Dictionary<String, object> post = timeline[i];
                Console.WriteLine(String.Format("{0} : {1}\n", i, post["text"]));
            }
        }

        /// <summary>
        /// Function that execute cursor query and send information for each query executed
        /// </summary>
        /// <param name="token"></param>
        static void ExecuteCursorQuery(Token token)
        {
            // The delegate is a function that will be called for each cursor
            Token.DynamicResponseDelegate del = delegate(dynamic jsonResponse, long previous_cursor, long next_cursor)
            {
                Console.WriteLine(previous_cursor + " -> " + next_cursor + " : " + jsonResponse.Count);
            };

            token.ExecuteCursorQuery("https://api.twitter.com/1/friends/ids.json?user_id=700562792", del);
        }
        #endregion

        #region ErrorHandling

        /// <summary>
        /// Testing the 3 ways to handle errors
        /// </summary>
        /// <param name="token"></param>
        static void test_error_functions(Token token)
        {
            integrated_error_handler(token);
            token_integrated_error_handler(token);
            execute_query_error_handler(token);
        }

        /// <summary>
        /// Initiating auto error handler
        /// You will not receive error information if handled by default error handler
        /// </summary>
        /// <param name="token"></param>
        static void integrated_error_handler(Token token)
        {
            token.Integrated_Exception_Handler = true;

            // Error is not automatically handled

            try
            {
                // Calling a method that does not exist
                dynamic timeline = token.ExecuteQuery("https://api.twitter.com/1/users/contributors.json?user_id=700562792");
            }
            catch (WebException wex)
            {
                Console.WriteLine("An error occured!");
                Console.WriteLine(wex);
            }
        }

        /// <summary>
        /// When assigning an error_handler to a Token think that it will be kept alive 
        /// until you specify it does not exist anymore by specifying :
        /// 
        /// token.Integrated_Exception_Handler = false;
        /// 
        /// You can assign null value if you do not want anything to be performed for you
        /// </summary>
        /// <param name="token"></param>
        static void token_integrated_error_handler(Token token)
        {
            token.ExceptionHandler = delegate(WebException wex)
            {
                Console.WriteLine("You received a Token generated error!");
                Console.WriteLine(wex.Message);
            };

            // Calling a method that does not exist
            dynamic timeline = token.ExecuteQuery("https://api.twitter.com/1/users/contributors.json?user_id=700562792");

            // Reset to basic Handler
            token.Integrated_Exception_Handler = false;
        }

        /// <summary>
        /// Uses the handler for only one query / work also for cursor queries
        /// </summary>
        /// <param name="token"></param>
        static void execute_query_error_handler(Token token)
        {
            Token.WebExceptionHandlingDelegate del = delegate(WebException wex)
            {
                Console.WriteLine("You received an execute_query_error!");
                Console.WriteLine(wex.Message);
            };

            dynamic timeline = token.ExecuteQuery("https://api.twitter.com/1/users/contributors.json?user_id=700562792", del);
        }

        #endregion

        #region Rate-Limit

        /// <summary>
        /// Enable you to get all information from Token and how many query you can execute
        /// Each time a query is executed the XRateLimitRemaining is updated.
        /// To improve efficiency, the other values are NOT.
        /// If you need these please call the function GetRateLimit()
        /// </summary>
        /// <param name="token"></param>
        static void get_rate_limit(Token token)
        {
            int remaining = token.GetRateLimit();
            Console.WriteLine("Used : " + remaining);
            Console.WriteLine("Used : " + token.XRateLimitRemaining);
            Console.WriteLine("Total per hour : " + token.XRateLimit);
            Console.WriteLine("Time before reset : " + token.ResetTimeInSeconds);
        }

        #endregion

        #endregion

        #region Stream

        private static List<Tweet> stream_list = new List<Tweet>();
        private static void processTweet(Tweet tweet, bool force = false)
        {
            if (tweet == null && !force)
                return;

            if (stream_list.Count % 125 != 124 && !force)
            {
                Console.WriteLine(tweet.user.name);
                stream_list.Add(tweet);
            }
            else
            {
                Console.WriteLine("Processing data");
                stream_list.Clear();
            }
        }

        private static void streaming_example(Token token)
        {
            // Creating a Delegate to use processTweet function to analyze each Tweet coming from the stream
            Stream.ProcessTweetDelegate produceTweetDelegate = new Stream.ProcessTweetDelegate(processTweet);
            // Creating the stream and specifying the delegate
            Stream myStream = new Stream(produceTweetDelegate);
            // Starting the stream by specifying credentials thanks to the Token
            myStream.StartStream(token);
        }
        #endregion

        #region User

        #region CreateUser
        public static void createUser(Token token, long id = 700562792)
        {
            User user = new User(id, token);
        }

        public static void createUserV2(Token token, long id = 700562792)
        {
            User user = new User(id);
            // Here we need to specify the token to retrieve the information
            // otherwise the information won't be filled
            user.FillUser(token);
        }
        #endregion

        #region Get Friends
        /// <summary>
        /// Show a the list of Friends from a userId
        /// </summary>
        /// <param name="token">Credentials</param>
        /// <param name="id">UserId to be analyzed</param>
        private static void getFriends(Token token, long id = 700562792)
        {
            User user = new User(id);
            var res = user.getFriends(token);
            Console.WriteLine("List of friends from " + id);

            foreach (long friend_id in res)
                Console.WriteLine(friend_id);
        }
        #endregion

        #region Get Followers
        private static void getFollowers(Token token, long? id = 700562792)
        {
            User user = new User((long)id);
            var res = user.getFollowers(token);

            Console.WriteLine(res.Count());
            foreach (long follower_id in res)
            {
                Console.WriteLine(follower_id);
            }
        }
        #endregion

        #region Get Profile Image

        static void getProfileImage(Token token)
        {
            User ladygaga = new User("ladygaga", token);
            Console.WriteLine(ladygaga.GetProfileImage(ImageSize.bigger, true));
        }

        #endregion

        #region Get Contributors

        static void getContributors(Token token, long id = 700562792, bool createContributorList = false)
        {
            User user = new User(id, token);
            List<User> contributors = user.GetContributors(createContributorList);
            List<User> contributorsAttribute = user.contributors;
            if (createContributorList && contributors != null)
            {
                if ((contributors == null && contributorsAttribute != null) || (contributors != null && contributorsAttribute == null)
                    || (!contributors.Equals(contributorsAttribute)))
                {
                    Console.WriteLine("The object attribute should be identical to the method result");
                }
            }
            if (contributors != null)
            {
                foreach (User c in contributors)
                {
                    Console.WriteLine("contributor id = " + c.id + " - screen_name = " + c.screen_name);
                }
            }
        }

        #endregion

        #region Get Contributees

        static void getContributees(Token token, long id = 700562792, bool createContributeeList = false)
        {
            User user = new User(id, token);
            List<User> contributees = user.GetContributees(createContributeeList);
            List<User> contributeesAttribute = user.contributees;
            if (createContributeeList)
            {
                if ((contributees == null && contributeesAttribute != null) || (contributees != null && contributeesAttribute == null)
                    || (!contributees.Equals(contributeesAttribute)))
                {
                    Console.WriteLine("The object attribute should be identical to the method result");
                }
            }
            if (contributees != null)
            {
                foreach (User c in contributees)
                {
                    Console.WriteLine("contributee id = " + c.id + " - screen_name = " + c.screen_name);
                }
            }

        }
        #endregion

        #endregion

        #region Tweet

        private static void create_tweet_with_entities(Token token)
        {
            // This tweet has classic entities
            Tweet tweet1 = new Tweet(127512260116623360, token);

            // This tweet has media entity
            try
            {
                Tweet tweet2 = new Tweet(112652479837110270, token);
            }
            catch (WebException wex)
            {
                Console.WriteLine("Tweet has not been created!");
            }
        }

        #endregion

        #region Tweetinvi API

        #endregion

        /// <summary>
        /// Run a basic application to provide a code example
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Initializing a Token with Twitter Credentials
            Token token = new Token()
            {
                AccessToken = ConfigurationManager.AppSettings["token_AccessToken"],
                AccessTokenSecret = ConfigurationManager.AppSettings["token_AccessTokenSecret"],
                ConsumerKey = ConfigurationManager.AppSettings["token_ConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["token_ConsumerSecret"]
            };

            Console.WriteLine("Creating Tweet from id");
            get_rate_limit(token);
            test_error_functions(token);

            Console.WriteLine("End");
            Console.ReadKey();
        }
        #endregion
    }
}
