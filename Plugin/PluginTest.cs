using System;

namespace Plugin
{
    public class PluginTest : SDG.Framework.Modules.IModuleNexus
    {
        public void initialize()
        {
            Console.WriteLine("------------------\nPLUGIN LOAD");

            //Get a hold of the chat
            /* Assembly-CSharp.dll
               ->SDG
               -->Unturned
               --->ChatManager
               ---->OnChatted
                   (SteamPlayer, EChatMode, Color, [bool]isRich, text, [bool]isVisible)
            */

            //Because the OnChatted call is directly using the delegate SDG.Unturned.Chatted, the easiest way to go about this
            //would probably be a lambda expression especially since it's heavily revolved around delegates and expression trees,
            //but fuck that cause honestly I've seen the worst of lambda and it's just something I don't want to get in to.
            //                             (Though this is closely related)
            SDG.Unturned.ChatManager.onChatted +=
                delegate (SDG.Unturned.SteamPlayer Player,
                         SDG.Unturned.EChatMode Mode,
                         ref UnityEngine.Color Color,
                         ref bool isRich,
                         string text,
                         ref bool isVisible)
                {
                    //Now let's make an example command handle
                    if (text[0] == '/') // Slashes are command signifiers
                    {
                        String[] InputArray = text.Split(' '); //Honestly makes it look easy but would've prefered stringstream
                        InputArray[0] = InputArray[0].Substring(1); //Remove the leading slash

                        switch (InputArray[0]) //Switch through our commands, not the brightest of ideas tbh but it'll work
                        {
                            case "broadcastA":
                                SDG.Unturned.ChatManager.say(text.Substring(InputArray[0].Length + 1), UnityEngine.Color.green);
                                break;
                            case "helpA":
                                SDG.Unturned.ChatManager.serverSendMessage("You are playing on a server with Plugin!",
                                                                           UnityEngine.Color.green, null, Player);
                                break;
                            case "website":
                                Player.player.sendBrowserRequest("Our Website", "http://example.com/");
                                break;
                            case "sAirdrop":
                                SDG.Unturned.LevelManager.airdropFrequency = 0;
                                break;
                        }
                    }
                };

            //Whenever someone is connecting, do this stuff.
            //Full tutorial on how I got this added, and how to work for yourself.
            SDG.Unturned.Provider.onServerConnected +=
                delegate (Steamworks.CSteamID steamID)
                {
                    Console.WriteLine(steamID.m_SteamID.ToString() + " is joining.");
                };

            //Whenever someone has left *Or are they leaving?*
            SDG.Unturned.Provider.onServerDisconnected +=
                delegate (Steamworks.CSteamID steamID)
                {
                    Console.WriteLine(steamID.m_SteamID.ToString() + " has left the building.");
                };

            SDG.Unturned.SteamChannel.onTriggerSend += HandleTriggerSend;

            Console.WriteLine("------------------");
        }

        //Params tryna make my life hard (Documented to the Wiki)
        void HandleTriggerSend(SDG.Unturned.SteamPlayer player,
                               string name,
                               SDG.Unturned.ESteamCall mode,
                               SDG.Unturned.ESteamPacket type,
                               params object[] arguments)
        {
            if (player != null)
                goto IL42;

            //Calls that come here:
            // -tellLightingWind
            // -tellLightingRain

            return;
            IL42:
            {
                //Player calls will come through here, examples to the Wiki

                if (name == "askAck")
                    return; //When the handshake is a lil too much for you to care

                if (name == "tellLean")
                    if (arguments[0] != null)
                    {
                        int L = ((byte)arguments[0] - 1);
                        if (L == 0)
                            return;
                        else
                            Console.WriteLine(player.player.name + " is leaning to the " + (L == -1 ? "Right" : "Left"));
                    }
            }
        }

        public void shutdown()
        {

        }
    }
}
