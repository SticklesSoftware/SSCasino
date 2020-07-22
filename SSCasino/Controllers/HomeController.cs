//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// HomeController.cs
//      This class implements a controller to handle web requests for the site.
//========================================================================================================================

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SSCasino.Models;

namespace SSCasino.Controllers
{
    public class HomeController : CommonController
    {
        #region Constants
        //================================================================================================================
        //================================================================================================================

        // Session Variables (Poker Video Lists)
        private const string PVL_BEATS = "PokerBeatsVids";
        private const string YT_LIMIT_EXCEEDED = "YouTube-Limit-Exceeded";

        // Callback Actions
        private const string ACT_SHUFFLE = "act_Shuffle";
        private const string ACT_CHANGE_CARD_PACK = "act_ChangeCardPack";
        private const string ACT_RESET = "act_Reset";
        private const string ACT_CHANGE_SAMPLE_SIZE = "act_ChangeSampleSize";
        private const string ACT_RESIZE_CHART = "act_ResizeChart";

        // Callback Arguments
        private const string ARG_ACTION = "arg_Action";
        private const string ARG_CARD_PACK_ID = "arg_CardPackId";
        private const string ARG_SHUFFLE_MODE = "arg_ShuffleMode";
        private const string ARG_SHUFFLE_TYPE = "arg_ShuffleType";
        private const string ARG_SHUFFLE_COUNT = "arg_ShuffleCount";
        private const string ARG_SAMPLE_SIZE = "arg_SampleSize";
        private const string ARG_CARD_AREA_WIDTH = "arg_CardAreaWidth";
        private const string ARG_CARD_AREA_HEIGHT = "arg_CardAreaHeight";
        private const string ARG_CARD_SAMPLES_HEIGHT = "arg_CardSamplesHeight";
        private const string ARG_SESSION_KEY = "arg_SessionKey";
        private const string ARG_SEARCH_PHRASE = "arg_SearchPhrase";
        private const string ARG_VIDEO_URL = "arg_VideoURL";
        private const string ARG_VIDEO_RESULT_NO = "arg_VideoResultNo";
        private const string ARG_CAT_NAME = "arg_CategoryName";

        // Google YouTube API
        private const string GOOGLE_ERROR_SOURCE = "Google.Apis";
        private const string YOU_TUBE_LINK = "https://www.youtube.com/embed/{0}";

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constants

        #region Shared
        //================================================================================================================
        //================================================================================================================

        public ActionResult MiniMenu()
        //================================================================================================================
        // This action is invoked when the site's mini-menu is requested.
        //
        // Returns
        //      The mini-menu partial view
        //================================================================================================================
        {
            return PartialView("_MiniMenu_Popup");
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Shared

        #region Home Page
        //================================================================================================================
        //================================================================================================================

        public ActionResult Home()
        //================================================================================================================
        // This action is invoked when the home page is requested.
        //
        // Returns
        //      The home page view
        //================================================================================================================
        {
            // Safety check for the server
            if ((bool)Session[SiteHelpers.ServerRunning] == false)
                return View("ServerNotRunning", new PokerPlayground());

            return View("Home", new PokerPlayground());
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Home

        #region Card Shuffler
        //================================================================================================================
        //================================================================================================================

        public ActionResult CardShuffler()
        //================================================================================================================
        // This action is invoked when the card shuffler page is requested.
        //
        // Event Arguments
        //      arg_CardPackId: Unique id of the selected card pack
        //
        // Returns
        //      The card shuffler page view
        //================================================================================================================
        {
            // Safety check for the server
            if ((bool)Session[SiteHelpers.ServerRunning] == false)
                return View("ServerNotRunning", new PokerPlayground());

            // Retrieve the selected card pack
            int cardPackId = !string.IsNullOrEmpty(Request.Params[ARG_CARD_PACK_ID]) ? int.Parse(Request.Params[ARG_CARD_PACK_ID]) : 0;

            // Create the model
            CardShuffler model = new CardShuffler();

            // Populate the model
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                dbCasino = new SSCasino_DBContext();

                // Get a card pack for the card shuffler
                // Load the shuffler control panel with card packs
                model.CardPack = CreateCardPackModel(dbCasino, cardPackId, SiteHelpers.SampleSizes.FullDeck);
                LoadShufflerCards(dbCasino, model.ControlPanel, cardPackId);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return View("CardShuffler", model);
        }

        public ActionResult CardShuffler_PerformAction()
        //================================================================================================================
        // This action is invoked when the card display area is performing an action on the cards.
        //
        // Event Arguments
        //      arg_Action:       Action to be performed
        //      arg_CardPackId:   Unique id of a card pack
        //      arg_ShuffleCount: Number of time to shuffle
        //      arg_ShuffleType:  Shuffle algorithm
        //
        // Returns
        //      The card display view
        //================================================================================================================
        {
            CardPack cardPack = null;

            // Retrieve the action to be performed and the assigned card pack
            string action = !string.IsNullOrEmpty(Request.Params[ARG_ACTION]) ? Request.Params[ARG_ACTION] : "";
            int cardPackId = !string.IsNullOrEmpty(Request.Params[ARG_CARD_PACK_ID]) ? int.Parse(Request.Params[ARG_CARD_PACK_ID]) : 0;

            // Connect to the database and perform the action
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                dbCasino = new SSCasino_DBContext();

                // Perform the action
                switch (action)
                {
                    case ACT_SHUFFLE:
                        // Retrieve an unshuffled pack of cards
                        cardPack = CreateCardPackModel(dbCasino, cardPackId);

                        // Shuffle the cards
                        int shuffleType = !string.IsNullOrEmpty(Request.Params[ARG_SHUFFLE_TYPE]) ? int.Parse(Request.Params[ARG_SHUFFLE_TYPE]) : 1;
                        int shuffleCount = !string.IsNullOrEmpty(Request.Params[ARG_SHUFFLE_COUNT]) ? int.Parse(Request.Params[ARG_SHUFFLE_COUNT]) : 1;
                        ShuffledPackage shuffledPackage = SiteHelpers.ShuffleCards(cardPack, (SiteHelpers.ShuffleTypes)shuffleType, shuffleCount, false);
                        cardPack = shuffledPackage.CardPack;
                        break;

                    case ACT_RESET:
                        // Get the default card pack
                        cardPack = CreateCardPackModel(dbCasino);
                        break;

                    case ACT_CHANGE_CARD_PACK:
                        // Retrieve an unshuffled pack of cards
                        cardPack = CreateCardPackModel(dbCasino, cardPackId);
                        break;

                    default:
                        // Get the default card pack
                        cardPack = CreateCardPackModel(dbCasino);
                        break;
                }
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return PartialView("_CardShuffler_DisplayCBP", cardPack.CardDeck);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Card Shuffler

        #region Randomness
        //================================================================================================================
        //================================================================================================================

        public ActionResult Randomness()
        //================================================================================================================
        // This action is invoked when the randomness comparison page is requested.
        //
        // Event Arguments
        //      arg_CardPackId: Unique id of the selected card pack
        //      arg_SampleSize: Unique id of the selected sample size
        //
        // Returns
        //      The card shuffler page view
        //================================================================================================================
        {
            // Safety check for the server
            if ((bool)Session[SiteHelpers.ServerRunning] == false)
                return View("ServerNotRunning", new PokerPlayground());

            // Retrieve the selected card pack
            int cardPackId = !string.IsNullOrEmpty(Request.Params[ARG_CARD_PACK_ID]) ? int.Parse(Request.Params[ARG_CARD_PACK_ID]) : 0;
            int sampleSize = !string.IsNullOrEmpty(Request.Params[ARG_SAMPLE_SIZE]) ? int.Parse(Request.Params[ARG_SAMPLE_SIZE]) : (int)SiteHelpers.SampleSizes.FourCardSample;

            // Create the model
            Randomness model = new Randomness();

            // Populate the model
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                dbCasino = new SSCasino_DBContext();

                // Get card packs for the card shuffler
                model.ShuffledPackageFisher.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                model.ShuffledPackageNaive.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);

                // Load the shuffler control panel with card packs
                LoadShufflerCards(dbCasino, model.ControlPanel, cardPackId);

                // Get the total number of shuffles and aggregated results
                GetShuffleResults(model.ShuffleResultsData);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return View("Randomness", model);
        }

        public ActionResult Randomness_PerformAction()
        //================================================================================================================
        // This action is invoked when the card display area is performing an action on the cards.
        //
        // Event Arguments
        //      arg_Action:       Action to be performed
        //      arg_CardPackId:   Unique id of a card pack
        //      arg_ShuffleCount: Number of time to shuffle
        //      arg_SampleSize:   Sample size of cards to shuffle
        //
        // Returns
        //      The card display view
        //================================================================================================================
        {
            // Retrieve the action to be performed and the event arguments
            string action = !string.IsNullOrEmpty(Request.Params[ARG_ACTION]) ? Request.Params[ARG_ACTION] : "";
            int cardPackId = !string.IsNullOrEmpty(Request.Params[ARG_CARD_PACK_ID]) ? int.Parse(Request.Params[ARG_CARD_PACK_ID]) : 0;
            int shuffleCount = !string.IsNullOrEmpty(Request.Params[ARG_SHUFFLE_COUNT]) ? int.Parse(Request.Params[ARG_SHUFFLE_COUNT]) : 0;
            int sampleSize = !string.IsNullOrEmpty(Request.Params[ARG_SAMPLE_SIZE]) ? int.Parse(Request.Params[ARG_SAMPLE_SIZE]) : 0;

            // Create the model
            Randomness model = new Randomness();

            // Perform the action
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                dbCasino = new SSCasino_DBContext();

                // Perform the action
                switch (action)
                {
                    case ACT_SHUFFLE:
                        // Get card packs for the shuffler
                        model.ShuffledPackageFisher.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                        model.ShuffledPackageNaive.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);

                        // Shuffle the cards
                        model.ShuffledPackageFisher = SiteHelpers.ShuffleCards(model.ShuffledPackageFisher.CardPack, SiteHelpers.ShuffleTypes.FisherYates, shuffleCount, true);
                        model.ShuffledPackageNaive = SiteHelpers.ShuffleCards(model.ShuffledPackageNaive.CardPack, SiteHelpers.ShuffleTypes.Naive, shuffleCount, true);

                        // Merge the Fisher-Yates and Naive shuffle results
                        SiteHelpers.CombinedShuffleResults = (ICollection<ShuffleResult>)model.ShuffledPackageFisher.ShuffleResults.Concat<ShuffleResult>(model.ShuffledPackageNaive.ShuffleResults).ToList();
                        break;

                    case ACT_CHANGE_CARD_PACK:
                        // Get card packs for the shuffler
                        model.ShuffledPackageFisher.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                        model.ShuffledPackageNaive.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                        break;

                    case ACT_CHANGE_SAMPLE_SIZE:
                        // Reset the shuffing results
                        // Get card packs based on sample size
                        SiteHelpers.ClearCombinedShuffleResults();
                        model.ShuffledPackageFisher.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                        model.ShuffledPackageNaive.CardPack = CreateCardPackModel(dbCasino, cardPackId, (SiteHelpers.SampleSizes)sampleSize);
                        break;

                    case ACT_RESET:
                        // Get default data for the page
                        ResetRandomnessPaage(dbCasino, model);
                        break;

                    default:
                        // Get default data for the page
                        ResetRandomnessPaage(dbCasino, model);
                        break;
                }

                // Always get the number of shuffles and the aggregated shuffle results
                model.ShuffleResultsData.ResizeWidth = (int)Session[SiteHelpers.ResultsGraphWidth];
                model.ShuffleResultsData.ResizeHeight = (int)Session[SiteHelpers.ResultsGraphHeight];
                GetShuffleResults(model.ShuffleResultsData);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return PartialView("_Randomness_DisplayCBP", model);
        }

        public ActionResult Randomness_RefreshResultsGraph()
        //================================================================================================================
        // This action is invoked when the shuffling results graph needs to be refreshed.
        //
        // Event Arguments
        //      arg_Action:       Action to be performed
        //      arg_ResizeWidth:  New size of the parent container
        //
        // Returns
        //      The sguffling results grasph
        //================================================================================================================
        {
            // Retrieve the action to be performed
            string action = !string.IsNullOrEmpty(Request.Params[ARG_ACTION]) ? Request.Params[ARG_ACTION] : "";

            // Create the model
            // Get the current size of the results graph
            ShuffleResultsGraph model = new ShuffleResultsGraph();
            if (Session[SiteHelpers.ResultsGraphWidth] != null)
            {
                model.ResizeWidth = (int)Session[SiteHelpers.ResultsGraphWidth];
                model.ResizeHeight = (int)Session[SiteHelpers.ResultsGraphHeight];
            }

            // Retrieve the shuffling results
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Perform the action
                switch (action)
                {
                    case ACT_RESIZE_CHART:
                        // Get the resize width and height
                        int cardAreaWidth = !string.IsNullOrEmpty(Request.Params[ARG_CARD_AREA_WIDTH]) ? int.Parse(Request.Params[ARG_CARD_AREA_WIDTH]) : 0;
                        int cardAreaHeight = !string.IsNullOrEmpty(Request.Params[ARG_CARD_AREA_HEIGHT]) ? int.Parse(Request.Params[ARG_CARD_AREA_HEIGHT]) : 0;
                        int cardSamplesHeight = !string.IsNullOrEmpty(Request.Params[ARG_CARD_SAMPLES_HEIGHT]) ? int.Parse(Request.Params[ARG_CARD_SAMPLES_HEIGHT]) : 0;

                        // Calculate the new width and height of the shuffling results graph
                        // Store these values in the sesssion object for use on refeshes
                        model.ResizeWidth = ((cardAreaWidth == 0) ? model.DeafultWidth : cardAreaWidth);
                        model.ResizeHeight = ((cardAreaHeight == 0) ? model.DefaultHeight : ((cardAreaHeight - cardSamplesHeight) - 30));
                        Session[SiteHelpers.ResultsGraphWidth] = model.ResizeWidth;
                        Session[SiteHelpers.ResultsGraphHeight] = model.ResizeHeight;
                        break;

                    case ACT_CHANGE_SAMPLE_SIZE:
                    case ACT_RESET:
                        // Reset the shuffing results
                        SiteHelpers.ClearCombinedShuffleResults();
                        break;

                    default:
                        // Reset the shuffing results
                        SiteHelpers.ClearCombinedShuffleResults();
                        break;
                }

                // Always get the results
                GetShuffleResults(model);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return PartialView("_Randomness_ResultsGraph", model);
        }

        private void ResetRandomnessPaage(SSCasino_DBContext dbCasino, Randomness model)
        //================================================================================================================
        // Reset the data for the randomness page
        //
        // Parameters
        //      dbCasino: Open connection to the database
        //      model:    Reference to a randomness model
        //================================================================================================================
        {
            // Reset the shuffing results
            // Get default card packs
            SiteHelpers.ClearCombinedShuffleResults();
            model.ShuffledPackageFisher.CardPack = CreateCardPackModel(dbCasino, sampleSize: SiteHelpers.SampleSizes.FourCardSample);
            model.ShuffledPackageNaive.CardPack = CreateCardPackModel(dbCasino, sampleSize: SiteHelpers.SampleSizes.FourCardSample);
        }

        private void GetShuffleResults(ShuffleResultsGraph model)
        //================================================================================================================
        // Get the collection of shuffle results since the last reset
        //
        // Parameters
        //      dbCasino: Open connection to the database
        //      model:    Reference to the randomness model to populate
        //================================================================================================================
        {
            model.TotalShuffles = 0;

            if (SiteHelpers.CombinedShuffleResults != null)
            {
                model.TotalShuffles = (SiteHelpers.CombinedShuffleResults.Count / 2);

                model.ShuffleResultsAggregated = SiteHelpers.CombinedShuffleResults
                    .GroupBy(g => new { g.ShuffleType, g.ShufflePattern })
                    .Select(e => new ShuffleResultAggregate
                    {
                        ShuffleType = e.Select(x => x.ShuffleType).FirstOrDefault(),
                        ShufflePattern = e.Select(x => x.ShufflePattern).FirstOrDefault(),
                        PatternCount = e.Count()
                    }).ToList();
            }
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Randomness

        #region Shuffler Control Panel
        //================================================================================================================
        //================================================================================================================

        public ActionResult ShufflerControlPanel_Reset()
        //================================================================================================================
        // This action is invoked when the shuffler control panel is being reset.
        //
        // Event Arguments
        //      arg_ShufflerMode: Operation mode for the shuffler
        //
        // Returns
        //      The shuffler control panel view
        //================================================================================================================
        {
            // Retrieve the operation mode for the shuffler
            SiteHelpers.ShuffleMode shuffleMode = (SiteHelpers.ShuffleMode)(!string.IsNullOrEmpty(Request.Params[ARG_SHUFFLE_MODE]) ? int.Parse(Request.Params[ARG_SHUFFLE_MODE]) : 1);

            // Create the model
            ShufflerControlPanel model = new ShufflerControlPanel(shuffleMode);

            // Load it with card packs
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                // Load it with all the card packs
                dbCasino = new SSCasino_DBContext();
                LoadShufflerCards(dbCasino, model, 0);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return PartialView("_Shuffler_ControlPanelCBP", model);
        }

        public ActionResult ShufflerControlPanel_ChangeCardPack()
        //================================================================================================================
        // This action is invoked when a new card pack is selected.
        //
        // Event Arguments
        //      arg_CardPackId: Unique id of a card pack
        //
        // Returns
        //      The card examples view
        //================================================================================================================
        {
            // Retrieve the id of the new card pack
            int cardPackId = !string.IsNullOrEmpty(Request.Params[ARG_CARD_PACK_ID]) ? int.Parse(Request.Params[ARG_CARD_PACK_ID]) : 0;
            CardPack model = null;

            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                // Create a card pack model for the specified card pack
                dbCasino = new SSCasino_DBContext();
                model = CreateCardPackModel(dbCasino, cardPackId: cardPackId);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return PartialView("_Shuffler_CardExamplesCBP", model);
        }

        private void LoadShufflerCards(SSCasino_DBContext dbCasino, ShufflerControlPanel shufflerCP, int cardPackId)
        //================================================================================================================
        // Load the shuffler control panel with card packs
        //
        // Parameters
        //      dbCasino:    Database connection
        //      shufflerCP:  Shuffler control panel
        //      cardPackId:  Unique id of the desired card pack or zero
        //      
        // Returns
        //      None
        //
        // Developer Notes
        //      The control panel object passed to this function is modified
        //================================================================================================================
        {
            // Load all card packs for then selection list
            shufflerCP.CardPacks = dbCasino.CardPacks.ToList();

            // If a card pack exists for the given id, use it
            // Otherwise the default card pack is used
            if (dbCasino.CardPacks.Any(e => e.CardPackId == cardPackId))
                shufflerCP.SelectedCardPack = dbCasino.CardPacks.Where(e => e.CardPackId == cardPackId).FirstOrDefault();
            else
                shufflerCP.SelectedCardPack = dbCasino.CardPacks.Where(e => e.DefaultPack == 1).FirstOrDefault();
        }

        private CardPack CreateCardPackModel(SSCasino_DBContext dbCasino, int cardPackId = 0, SiteHelpers.SampleSizes sampleSize = SiteHelpers.SampleSizes.FullDeck)
        //================================================================================================================
        // Retrieve a pack of cards based on the given pack id
        //
        // Parameters
        //      dbCasino:   Open Connection to the database
        //      cardPackId: (optional) Unique identifier for a card pack
        //      sampleSize: (optional) Number of cards to load
        //      
        // Returns
        //      A full or partial pack of cards
        //================================================================================================================
        {
            // Create the model
            CardPack model = new CardPack();

            switch (sampleSize)
            {
                case SiteHelpers.SampleSizes.FourCardSample:
                    string[] fourSampleCards = SiteHelpers.FourCardSampleCards.Split(' ');
                    if (dbCasino.CardPacks.Any(e => e.CardPackId == cardPackId))
                    {
                        model = dbCasino.CardPacks.Where(e => e.CardPackId == cardPackId).FirstOrDefault();
                        model.CardDeck = dbCasino.PlayingCardsView.Where(e => ((e.CardPackId == cardPackId) && (fourSampleCards.Contains(e.CardCode)))).OrderBy(e => e.CardSuitOrder).ToList();
                    }
                    else
                    {
                        model = dbCasino.CardPacks.Where(e => e.DefaultPack == 1).FirstOrDefault();
                        model.CardDeck = dbCasino.PlayingCardsView.Where(e => ((e.DefaultPack == 1) && (fourSampleCards.Contains(e.CardCode)))).OrderBy(e => e.CardSuitOrder).ToList();
                    }
                    break;

                case SiteHelpers.SampleSizes.SixCardSample:
                    string[] sixSampleCards = SiteHelpers.SixCardSampleCards.Split(' ');
                    if (dbCasino.PlayingCardsView.Any(e => e.CardPackId == cardPackId))
                    {
                        model = dbCasino.CardPacks.Where(e => e.CardPackId == cardPackId).FirstOrDefault();
                        model.CardDeck = dbCasino.PlayingCardsView.Where(e => ((e.CardPackId == cardPackId) && (sixSampleCards.Contains(e.CardCode)))).OrderBy(e => e.CardSuitOrder).ToList();
                    }
                    else
                    {
                        model = dbCasino.CardPacks.Where(e => e.DefaultPack == 1).FirstOrDefault();
                        model.CardDeck = dbCasino.PlayingCardsView.Where(e => ((e.DefaultPack == 1) && (sixSampleCards.Contains(e.CardCode)))).OrderBy(e => e.CardSuitOrder).ToList();
                    }
                    break;

                case SiteHelpers.SampleSizes.FullDeck:
                    if (dbCasino.PlayingCardsView.Any(e => e.CardPackId == cardPackId))
                        model = dbCasino.CardPacks.Include("CardDeck").Where(e => e.CardPackId == cardPackId).FirstOrDefault();
                    else
                        model = dbCasino.CardPacks.Include("CardDeck").Where(e => e.DefaultPack == 1).FirstOrDefault();
                    break;
            }

            return model;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Shuffler Control Panel

        #region API Demos
        //================================================================================================================
        //================================================================================================================

        public ActionResult APIDemos()
        //================================================================================================================
        // This action is invoked when the APIs demo page is requested.
        //
        // Returns
        //      The APIs demo view
        //================================================================================================================
        {
            // Safety check for the server
            if ((bool)Session[SiteHelpers.ServerRunning] == false)
                return View("ServerNotRunning", new PokerPlayground());

            // Create the model
            APIDemos model = new APIDemos();

            // Add a YouTube search request for the default category and get results
            model.YouTubeAPI.SearchRequest = new YouTubeSearchRequest(PVL_BEATS, Properties.Resources.PokerBeats_Phrase);
            model.YouTubeAPI.DataLimitExceeded = GetSerachResults(model.YouTubeAPI, Properties.Resources.PokerBeats_Caption, model.YouTubeAPI.SearchRequest);

            return View("APIDemos", model);
        }

        public ActionResult APIDemos_RefreshThumbnails()
        //================================================================================================================
        // This action is invoked when the thumbnails area is requested.
        //
        // Event Arguments
        //      arg_SessionKey:   Search request key
        //      arg_SearchPhrase: YouTube search phrase
        //      arg_CategoryName: Category being searched
        //
        // Returns
        //      Thumbnails area call back panel
        //================================================================================================================
        {
            // Retrieve the search request data
            string sessionKey = (!string.IsNullOrEmpty(Request.Params[ARG_SESSION_KEY]) ? Request.Params[ARG_SESSION_KEY] : PVL_BEATS);
            string searchPhrase = (!string.IsNullOrEmpty(Request.Params[ARG_SEARCH_PHRASE]) ? Request.Params[ARG_SEARCH_PHRASE] : Properties.Resources.PokerBeats_Phrase);
            string categoryName = (!string.IsNullOrEmpty(Request.Params[ARG_CAT_NAME]) ? Request.Params[ARG_CAT_NAME] : Properties.Resources.PokerBeats_Caption);

            // Create the model
            // Add a YouTube search request for the specified category and get results
            YouTubeAPI model = new YouTubeAPI
            {
                SearchRequest = new YouTubeSearchRequest(sessionKey, searchPhrase)
            };
            model.DataLimitExceeded = GetSerachResults(model, categoryName, model.SearchRequest);

            return PartialView("_APIDemos_ThumbnailsCBP", model);
        }

        public ActionResult APIDemos_LoadNewVideo()
        //================================================================================================================
        // This action is invoked to load a new video into the player.
        //
        // Event Arguments
        //      arg_CategroyName: Categroy name of the requested video
        //      arg_VideoId:      Video id of requested video
        //
        // Returns
        //      Video player call back panel
        //================================================================================================================
        {
            // Retrieve the requested video id
            string videoURL = (!string.IsNullOrEmpty(Request.Params[ARG_VIDEO_URL]) ? Request.Params[ARG_VIDEO_URL] : "");
            int videoResultNo = !string.IsNullOrEmpty(Request.Params[ARG_VIDEO_RESULT_NO]) ? int.Parse(Request.Params[ARG_VIDEO_RESULT_NO]) : 0;

            // Create the model
            YouTubeAPI model = new YouTubeAPI
            {
                SelectedVideoURL = videoURL,
                SelectedVideoResultNo = videoResultNo
            };

            return PartialView("_APIDemos_VideoPlayerCBP", model);
        }

        private bool GetSerachResults(YouTubeAPI youTubeAPI, string categoryName, YouTubeSearchRequest searchRequest)
        //================================================================================================================
        // Retrieve search results based on the given search request
        //
        // Parameters
        //      youTubeAPI:    YouTube API  options
        //      categoryName:  Category being searched
        //      searchRequest: YouTube search request data
        //
        // Retruns
        //      True/False, has the YouTube data limit been exceeded
        //
        // Outputs
        //      The SearchResults list in the searchRequest object is populated
        //
        // Developer Notes
        //      To avoid exceeding the YouTube data limits, search results are chached locally in the session and also
        //      written to the database. Retrieval priority is as follows...
        //          1) Results are taken from the local cache, if available
        //          2) Results are retrieved from YouTube
        //          3) Results are retrieved from the database, If the data limit has been exceeded
        //
        //          NOTE: A data limnit exceeded flag is also stored in the session. This value is checked first, before
        //                connecting to YouTube to avoid unnecessary data limit exceeded errors.
        //================================================================================================================
        {
            bool dataLimitExceeded = false;

            // Results are loaded from the database...
            //      If the developer flag UseDatabaseFirst is set
            //      -- OR --
            //      If our YouTube API data limit has been exceeded and the local cache is empty
            if ((youTubeAPI.UseDatabaseFirst) || ((youTubeAPI.DataLimitExceeded) && (Session[searchRequest.SessionKey] == null)))
                dataLimitExceeded = (GetSerachResultsFromDatabase(searchRequest) == false);
            else
            {
                // Results are taken from the local cache, if available
                if (Session[searchRequest.SessionKey] != null)
                    searchRequest.SearchResults = (List<YouTubeSearchResult>)Session[searchRequest.SessionKey];
                else
                {
                    // If the YouTube data limit has not been exceeded, Results are retrieved from YouTube
                    // Otherwise, results are retrieved from the database
                    dataLimitExceeded = (Session[YT_LIMIT_EXCEEDED] != null);
                    if (!dataLimitExceeded)
                    {
                        // If the data limit has been exceeded, results are retrieved from the database, if available
                        dataLimitExceeded = SearchYouTube(searchRequest);
                        if (dataLimitExceeded)
                        {
                            dataLimitExceeded = (GetSerachResultsFromDatabase(searchRequest) == false);
                            Session[YT_LIMIT_EXCEEDED] = true;
                        }
                    }
                }
            }

            // Select the default video
            if (!dataLimitExceeded) {
                youTubeAPI.SelectedCategory = String.Format(Properties.Resources.PokerTopTen, categoryName);
                youTubeAPI.SelectedVideoURL = searchRequest.SearchResults[0].YouTubeURL;
                youTubeAPI.SelectedVideoResultNo = searchRequest.SearchResults[0].ResultNo;
            }

            return dataLimitExceeded;
        }

        private bool SearchYouTube(YouTubeSearchRequest searchRequest)
        //================================================================================================================
        // Search YouTube based on the given search request and populate the request with search results
        //
        // Parameters
        //      searchRequest: YouTube search request data
        //
        // Retruns
        //      True/False, has the YouTube data limit been exceeded
        //================================================================================================================
        {
            bool dataLimitReached = false;

            try
            {
                // Create and initialize the YouTube API
                YouTubeService youTube = new YouTubeService(
                        new BaseClientService.Initializer()
                        {
                            ApiKey = "na",
                            ApplicationName = this.GetType().ToString()
                        });

                // Build a search request for the selected category
                SearchResource.ListRequest youtubeRequest = youTube.Search.List("snippet");
                youtubeRequest.Q = searchRequest.SearchPhrase;
                youtubeRequest.Type = "video";
                youtubeRequest.MaxResults = searchRequest.MaxResults;

                // Search YouTube and parse the results
                SearchListResponse searchResults = youtubeRequest.Execute();
                ParseYouTubeSearchResults(searchRequest, searchResults);
                SaveYouTubeSearchResults(searchRequest);
            }
            catch (Exception runError)
            {
                if (runError.Source == GOOGLE_ERROR_SOURCE)
                    dataLimitReached = true;
            }

            return dataLimitReached;
        }

        private void ParseYouTubeSearchResults(YouTubeSearchRequest searchRequest, SearchListResponse searchResults)
        //================================================================================================================
        // Parse the given YouTube search results into a search results list
        //
        // Parameters
        //      searchRequest: YouTube search request data
        //      searchResults: YouTube search results data
        //
        // Outputs
        //      The SearchResults list in the searchRequest object is populated
        //================================================================================================================
        {
            int resultCount = 0;

            foreach (SearchResult searchResult in searchResults.Items)
            {
                // Bump the result count
                resultCount++;

                // Create and initialize a new YouTube search result
                YouTubeSearchResult item = new YouTubeSearchResult
                {
                    SessionKey = searchRequest.SessionKey,
                    ResultNo = resultCount,
                    YouTubeVideoId = searchResult.Id.VideoId,
                    Title = searchResult.Snippet.Title,
                    Description = searchResult.Snippet.Description,
                    ThumbnailURL = searchResult.Snippet.Thumbnails.Default__.Url,
                    YouTubeURL = string.Format(YOU_TUBE_LINK, searchResult.Id.VideoId)
                };

                // Add this result to the collection
                searchRequest.SearchResults.Add(item);
            }
        }

        private void SaveYouTubeSearchResults(YouTubeSearchRequest searchRequest)
        //================================================================================================================
        // Save the given YouTube search results to the session and database
        //
        // Parameters
        //      searchRequest: YouTube search request data populated with results
        //================================================================================================================
        {

            // Save the results locally in the session
            Session[searchRequest.SessionKey] = searchRequest.SearchResults;

            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect to the database
                dbCasino = new SSCasino_DBContext();

                // If the results are is NOT in the database, save them
                if (!dbCasino.YouTubeSearchResults.Any(e => e.SessionKey == searchRequest.SessionKey))
                {
                    // Save the results to the database
                    dbCasino.YouTubeSearchResults.AddRange(searchRequest.SearchResults);
                    dbCasino.SaveChanges();
                }
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }
        }

        private bool GetSerachResultsFromDatabase(YouTubeSearchRequest searchRequest)
        //================================================================================================================
        // Retrieve search results from the database based on the given search request
        //
        // Parameters
        //      searchRequest: YouTube search request data
        //
        // Outputs
        //      The SearchResults list in the searchRequest object is populated
        //
        // Retruns
        //      True/False, has the YouTube data limit been exceeded
        //================================================================================================================
        {
            bool dataAvailable = false;

            // Populate the model
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect the the database
                dbCasino = new SSCasino_DBContext();

                // Retrieve results frokm the database
                searchRequest.SearchResults = dbCasino.YouTubeSearchResults.Where(e => e.SessionKey == searchRequest.SessionKey).ToList();
                dataAvailable = (searchRequest.SearchResults.Count != 0);
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return dataAvailable;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // API Demos

        #region Poker Room
        //================================================================================================================
        //================================================================================================================

        public ActionResult PokerRoom()
        //================================================================================================================
        // This action is invoked when the poker room page is requested.
        //
        // Returns
        //      The home page view
        //================================================================================================================
        {
            // Safety check for the server
            if ((bool)Session[SiteHelpers.ServerRunning] == false)
                return View("ServerNotRunning", new PokerPlayground());

            PokerRoom model = new PokerRoom();

            return View("PokerRoom", model);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Poker Room
    }
}
