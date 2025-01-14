﻿using System;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Core;
using System.Collections.Generic;
using Logger = Rocket.Core.Logging.Logger;

namespace fr34kyn01535.MessageAnnouncer
{
    public class MessageAnnouncer : RocketPlugin<MessageAnnouncerConfiguration>
    {
        public int lastindex = 0;
        public DateTime? lastmessage = null;

        void FixedUpdate()
        {
            printMessage();
        }

        private List<RocketTextCommand> commands = new List<RocketTextCommand>();

        protected override void Load()
        {
            if (Configuration != null && Configuration.Instance.TextCommands != null)
            {
                foreach (TextCommand t in Configuration.Instance.TextCommands)
                {
                    RocketTextCommand command = new RocketTextCommand(t.Name, t.Help, t.Text);
                    commands.Add(command);
                    R.Commands.Register(command);
                }
            }
        }

        protected override void Unload()
        {
            foreach (RocketTextCommand command in commands)
            {
                R.Commands.DeregisterFromAssembly(this.Assembly);
            }
            commands.Clear();
        }

        private void printMessage()
        {
            try
            {
                if (State == Rocket.API.PluginState.Loaded && Configuration.Instance.Messages != null && (lastmessage == null || ((DateTime.Now - lastmessage.Value).TotalSeconds > Configuration.Instance.Interval)))
                {
                    if (lastindex > (Configuration.Instance.Messages.Length - 1)) lastindex = 0;
                    Message message = Configuration.Instance.Messages[lastindex];
                    ChatManager.serverSendMessage(message.Text, UnturnedChat.GetColorFromName(message.Color, Color.green), iconURL: message.IconURL, useRichTextFormatting: true);
                    Logger.Log(message.Text);
                    lastmessage = DateTime.Now;
                    lastindex++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
