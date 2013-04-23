using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Models.Steam.Heroes;
using DotaStatsWebApi.Models.Steam.Matches;
using DotaStatsWebApi.Models.Steam.Players;
using Newtonsoft.Json;
using System.Text;

namespace DotaStatsWebApi.SteamApi
{
    public class SteamApiConnector : WebClient
    {
        private static readonly string key = "key=84D99D637A49766C4725E98DE758BD4D";
        private static readonly string baseMatchUrl = "https://api.steampowered.com/IDOTA2Match_570/";
        private static readonly string basePlayerUrl = "http://api.steampowered.com/ISteamUser/";

        // example
        // https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=84D99D637A49766C4725E98DE758BD4D
        public List<Match> Get25MostRecentMatches()
        {
            var middlerUrl = "GetMatchHistory/V001/?";
            var fullUrl = baseMatchUrl + middlerUrl + key;
            var matchJson = Encoding.Default.GetString(DownloadData(fullUrl));

            var matchSteamResult = JsonConvert.DeserializeObject<MatchHistorySteamResult>(matchJson);
            return matchSteamResult.result.matches;

        }

        // example:
        // https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=176972163&key=84D99D637A49766C4725E98DE758BD4D
        public Match TryGetMatchDetails(long matchId)
        {
            try
            {
                var middlerUrl = "GetMatchDetails/V001/?match_id=";
                var fullUrl = baseMatchUrl + middlerUrl + matchId.ToString() + "&" + key;
                var matchJson = System.Text.Encoding.Default.GetString(DownloadData(fullUrl));

                var matchDetails = JsonConvert.DeserializeObject<MatchDetailsSteamResult>(matchJson);
                return matchDetails.result;
            }
            catch { return null; }
        }

        /*public MatchHistory getInitialMatchHistory(int steamId32)
        {
            string historyJson = string.Empty;
            string fullUrl = string.Empty;
            MatchHistory matchHistory = new MatchHistory();

            BaseAddress = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?account_id=";
            fullUrl = string.Format("{0}{1}&key=84D99D637A49766C4725E98DE758BD4D", BaseAddress, steamId32);
            historyJson = System.Text.Encoding.Default.GetString(DownloadData(fullUrl));

            matchHistory = JsonConvert.DeserializeObject<MatchHistory>(historyJson);

            return matchHistory;
        }*/

        /// <summary>
        /// Gets the next 25 match histories from Valve API
        /// </summary>
        /// <param name="steamId32">32 bit id of steam user</param>
        /// <param name="startMatch">Number of match to start at (goes from highest to lowest)</param>
        /*public MatchHistory populateRemainingMatchHistory(int steamId32, string startMatch)
        {
            string historyJson = string.Empty;
            string fullUrl = string.Empty;
            MatchHistory matchHistory = new MatchHistory();

            BaseAddress = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?";
            fullUrl = string.Format("{0}account_id={1}&start_at_match_id={2}&key=84D99D637A49766C4725E98DE758BD4D", BaseAddress, steamId32, startMatch);
            historyJson = System.Text.Encoding.Default.GetString(DownloadData(fullUrl));

            matchHistory = JsonConvert.DeserializeObject<MatchHistory>(historyJson);

            return matchHistory;
        }*/

        public HeroSteamResult getHeroInfo()
        {
            string heroJson = string.Empty;
            string fullUrl = string.Empty;
            HeroSteamResult heroList = new HeroSteamResult();

            BaseAddress = "https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?";
            fullUrl = string.Format("{0}key=84D99D637A49766C4725E98DE758BD4D&language=en_us", BaseAddress);
            heroJson = System.Text.Encoding.Default.GetString(DownloadData(fullUrl));

            heroList = JsonConvert.DeserializeObject<HeroSteamResult>(heroJson);

            return heroList;
        }

        // example:
        // https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=84D99D637A49766C4725E98DE758BD4D&steamids=76561198066148159
        public Player GetPlayerInfo(int accountId)
        {
            var middleUrl = "GetPlayerSummaries/v0002/?";
            Int64 steamId = accountId + 76561197960265728;
            var fullUrl = basePlayerUrl + middleUrl + key + "&steamids=" + steamId;
            
            var playerJson = Encoding.Default.GetString(DownloadData(fullUrl));

            var steamResponse = JsonConvert.DeserializeObject<PlayerSteamResponse>(playerJson);
            var playerInfo = steamResponse.response.players.First();

            return playerInfo;
        }
    }
}