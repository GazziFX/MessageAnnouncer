using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace fr34kyn01535.MessageAnnouncer
{
    public sealed class TextCommand
    {
        public string Name;
        public string Help;
        [XmlArrayItem("Line")]
        public List<string> Text;
    }

    public sealed class Message
    {
        [XmlAttribute("Text")]
        public string Text;

        [XmlAttribute("Color")]
        public string Color;

        [XmlAttribute("IconURL")]
        public string IconURL;

        public Message(string text, string color, string iconurl)
        {
            Text = text;
            Color = color;
            IconURL = iconurl;
        }
        public Message()
        {
            Text = "";
            Color = "";
            IconURL = "";
        }
    }

    public class MessageAnnouncerConfiguration : IRocketPluginConfiguration
    {
        public int Interval;

        [XmlArrayItem("Message")]
        [XmlArray(ElementName = "Messages")]
        public Message[] Messages;

        [XmlArrayItem("TextCommand")]
        [XmlArray(ElementName = "TextCommands")]
        public List<TextCommand> TextCommands;

        public void LoadDefaults()
        {
            Interval = 180;
            Messages = new Message[]{
                new Message("Welcome to server, we hope you enjoy your stay!","Green", "https://i.imgur.com/WbY6CMj.png"),
                new Message("Please chat in english. Be polite.","Green", ""),
            };
            TextCommands = new List<TextCommand>(){
                new TextCommand(){Name="rules",Help="Shows the server rules",Text = new List<string>(){
                    "#1 No offensive content in the chat, respect other players",
                    "#2 No bug using, exploiting or abuse of powers",
                    "#3 Don't ask admins for items, teleports, loot respawn, ect.",
                    "#4 Please speak english in the public chat"}
                }
            };
        }
    }
}
