﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace UkRegVoteBot
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "@UKRegToVote Twitter Bot";

            try {
                auth authDetails = new auth();
                Auth.SetUserCredentials(authDetails.consumerKey, authDetails.consumerSecret, authDetails.accessToken, authDetails.accessSecret);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log("Connect failed!", "error");
                Log(ex.ToString(), "error");
            }
            finally
            {
                Log("Connect success!", "success");
                //Tweet.PublishTweet("test");

            }
            iDontKnowWhatToNameThisMethod();
            Console.ReadLine();
        }

        static void iDontKnowWhatToNameThisMethod()
        {

            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;

            /*
             * 
             * REGISTERING TO VOTE
             * Til 22nd May
             * 
             */

            string[] hashtags = new string[]
            {
                "#GeneralElection",
                "#GE2017",

            };

            string[] tweetsReg = new string[]
            {
                "Remember to register to vote before it's too late!",
                "Have you changed your address since the last election you voted in? You need to re-register!",
                "Registering to vote usually only takes 5 minutes of your time. Do it before you forget!",
                "Just turned 18? Is this the first election you'll vote in? Remember to register!",
                "If you're having exams on the day of the election, you can sign up for a postal vote!",
                "Student? You can register to vote at home and your Uni accommodation",
                "Outside of the UK on the 8th of June? You can still register for a postal vote!",
                "You only have until the 22nd of May to register!",
                "If you're 16-17, you can still register even though you can't yet vote!",
                "This is your last chance to register to vote. Please do it if you haven't already!",
                "Register to vote now to have your say in the 2017 General Election",
                "Unhappy with the current government? Register to vote to have your say in the election!",
                "No matter where you stand politically, you must register to vote in order to have your say",
                "You can have say in who runs this country if you register to vote before midnight 22/05",
                "Don't think you'll be able to make it to the station on voting day? Get a postal vote!",
                "Even if you've already registered, you must update any details changed since last time!",
                "You only have until 11:59 pm on the 22nd to register!"
            };



            /*
             * 
             * DAY OF VOTE
             * 8th June 2017
             * 7am-10pm
             * 
             */

            string hrsLeft = ((22 - 7) - hour).ToString();
            string hrsLeftStatement = hrsLeft + " hours left!";
            string footVote = hrsLeftStatement;

            string[] tweetsVote = new string[]
            {
                "Remember to go out and vote to make your voice heard!",
                "Remember to vote if you haven't already! ",
                "Vote!",
                "Vote vote vote!",
            };

            /*
             * 
             * Date Checking
             * 
             */

            while (true) {

                // if before reg deadline
                if (month < 5 || (date < 23 && month == 5))
                {
                    //SendTweet(tweetsReg, 60, hashtags);
                    SendTweet(tweetsReg, 15, hashtags);
                }
                // if between 7pm-10pm on vote day
                else if(date == 8 && month == 6 && hour >= 7 && hour <= 22)
                {
                    SendTweet(tweetsVote, 30, hashtags);
                }

            }

        }

        static void SendTweet(string[] tweetArray, int interval, string[] hashtags)
        {

            int min = DateTime.Now.Minute;


            Random random = new Random();

            // if
            // specified tweet every 60 mins and it's minute 0
            // or
            // specified tweet every 30 mins and it's miniute 0 or 40

            if ((interval == 60 && min == 0) || (interval == 30 && (min == 0 || min == 30)) || (interval == 15 && (min % 5 == 0)))
            {

                string chosenTweet = tweetArray[random.Next(tweetArray.Length)] + " " + hashtags[random.Next(hashtags.Length)];
                string chosenWithUrl = chosenTweet + " https://www.gov.uk/register-to-vote ";
                ;

                // try to tweet
                try
                {
                    Tweet.PublishTweet(chosenWithUrl);
                }
                // catch error
                catch (Exception ex)
                {
                    Log("Tweet failed!", "error");
                    Log(ex.ToString(), "error");
                }
                // detail success + tweet posted
                finally
                {
                    Log("Tweet success! Length: " + (chosenTweet.Length + 23), "success");
                    Log(chosenWithUrl, "");
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(30000);
                }

            }
            System.Threading.Thread.Sleep(30000);

        }

        static void Log(string msg, string type)
        {
            if(type.ToLower() == "error")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if(type.ToLower() == "success")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine(DateTime.Now + "  |  " + msg);
        }

        
    }
}
