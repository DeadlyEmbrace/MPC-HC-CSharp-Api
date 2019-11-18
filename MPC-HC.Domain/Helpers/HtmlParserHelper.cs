using System;
using HtmlAgilityPack;

namespace MPC_HC.Domain.Helpers
{
    public static class HtmlParserHelper
    {
        public static Info ParseHtmlToInfo(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            htmlDoc.LoadHtml(htmlDoc.DocumentNode.OuterHtml); //yes this is needed....
            var info = new Info
            {
                FileName = htmlDoc.GetElementbyId("file").InnerText,
                FilePathArg = htmlDoc.GetElementbyId("filepatharg").InnerText,
                FilePath = htmlDoc.GetElementbyId("filepath").InnerText,
                FileDirArg = htmlDoc.GetElementbyId("filedirarg").InnerText,
                FileDir = htmlDoc.GetElementbyId("filedir").InnerText,
                State = Info.IntToState(Convert.ToInt32(htmlDoc.GetElementbyId("state").InnerText)),
                StateString = htmlDoc.GetElementbyId("statestring").InnerText,
                Position = TimeSpan.FromMilliseconds(Convert.ToDouble(htmlDoc.GetElementbyId("position").InnerText)),
                PositionMillisec = Convert.ToInt64(htmlDoc.GetElementbyId("position").InnerText),
                Duration = TimeSpan.FromMilliseconds(Convert.ToDouble(htmlDoc.GetElementbyId("duration").InnerText)),
                DurationMillisec = Convert.ToInt64(htmlDoc.GetElementbyId("duration").InnerText),
                VolumeLevel = Convert.ToInt32(htmlDoc.GetElementbyId("volumelevel").InnerText),
                Muted = Convert.ToBoolean(Convert.ToInt16(htmlDoc.GetElementbyId("muted").InnerText)),
                PlaybackRate = Convert.ToDouble(htmlDoc.GetElementbyId("playbackrate").InnerText),
                SizeString = htmlDoc.GetElementbyId("size").InnerText,
                ReloadTime = Convert.ToInt32(htmlDoc.GetElementbyId("reloadtime").InnerText),
                Version = htmlDoc.GetElementbyId("version").InnerText
            };

            var apiVersion = htmlDoc.GetElementbyId("apiversion");
            info.ApiVersion = apiVersion == null 
                ? 0 
                : int.Parse(apiVersion.InnerText);

            // These values are only available for API version >= 1
            if (info.ApiVersion >= 1)
            {
                info.FullScreen = bool.Parse(htmlDoc.GetElementbyId("fullscreen").InnerText);
            }

            return info;
        }
    }
}