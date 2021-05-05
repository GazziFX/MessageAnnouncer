using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace fr34kyn01535.MessageAnnouncer
{
    public class RocketTextCommand : IRocketCommand
    {
        private List<string> text;
        private string name;
        private string help;
        

        public RocketTextCommand(string commandName,string commandHelp,List<string> text)
        {
            name = commandName;
            help = commandHelp;
            this.text = text;
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var uplayer = caller as UnturnedPlayer;
            if (uplayer == null)
                return;

            foreach (string l in text)
            {
                ChatManager.serverSendMessage(l, Color.white, toPlayer: uplayer.SteamPlayer(), useRichTextFormatting: true);
            }
        }

        public string Help
        {
            get { return help; }
        }

        public string Name
        {
            get { return name; }
        }

        public List<string> Permissions
        {
            get { return new List<string>(); }
        }

        public string Syntax
        {
            get { return ""; }
        }


        public AllowedCaller AllowedCaller
        {
            get { return Rocket.API.AllowedCaller.Player; }
        }
    }
}
