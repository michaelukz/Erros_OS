﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Erros;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;

namespace Erros.Errors
{
    public class Logs : ModuleBase<SocketCommandContext>
    {
        
        //// Autonomous Virtual Builders \\\\
        /// <summary>
        /// Log: AVB-01 A user has been banned.
        /// </summary>
        public static EmbedBuilder avb01(SocketUser user,string reason,SocketUser admin)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{user.Username} has just been banned!."),
                Description = ($"Banned by: {admin.Mention}"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media.giphy.com/media/RAUh1XkOJnF4c/giphy.gif")
            };
            e.AddField("Reason:",$"{reason}",false);
            return e;
        }
        /// <summary>
        /// Log: AVB-01 Server's prefix has changed.
        /// </summary>
        public static EmbedBuilder avb02(string prefix)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"Your server's prefix has changed."),
                Description = ($"The new prefix is: {prefix}\nThat's a good way to shake it up!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media1.giphy.com/media/C8gbjxvAP6Su7Zlg2e/giphy.gif?cid=3640f6095c1d892345676437674ae489")
            };
            return e;
        }
        /// <summary>
        /// Log: AVB-03 A role has been given a permission.
        /// </summary>
        public static EmbedBuilder avb03(SocketRole role,string nperm)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{role.Name} role has been given permission to use the {nperm} command"),
                Description = ($"Ooooh, New permission...Gimme."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media3.giphy.com/media/12Eo7WogCAoj84/giphy.gif?cid=3640f6095c1e2bb32f57617951623836")
            };
            return e;
        }
        /// <summary>
        /// Log: AVB-04 A user has been given a permission.
        /// </summary>
        public static EmbedBuilder avb04(SocketGuildUser user, string nperm)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{user.Username} has been given permission to use the {nperm} command"),
                Description = ($"Ooooh, New permission...Gimme."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media3.giphy.com/media/12Eo7WogCAoj84/giphy.gif?cid=3640f6095c1e2bb32f57617951623836")
            };
            return e;
        }
        /// <summary>
         /// Log: AVB-05 A user's permission has been taken.
         /// </summary>
        public static EmbedBuilder avb05(SocketGuildUser user, string perm)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{user.Username} permission to the {perm} has been revoked."),
                Description = ($"Yay...It's mine now!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media2.giphy.com/media/4KELApWfGG3wt5xyLD/giphy.gif?cid=3640f6095c1e6bf4366e6e5877e44148")
            };
            return e;
        }
        /// <summary>
        /// Log: AVB-06 A roles's permission has been taken.
        /// </summary>
        public static EmbedBuilder avb06(SocketRole role, string perm)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{role.Name}' permission to the {perm} has been revoked."),
                Description = ($"Yay...It's mine now!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media2.giphy.com/media/4KELApWfGG3wt5xyLD/giphy.gif?cid=3640f6095c1e6bf4366e6e5877e44148")
            };
            return e;
        }
        /// <summary>
         /// Log: AVB-07 A user's has been kicked.
         /// </summary>
        public static EmbedBuilder avb07(SocketGuildUser user, string reason,SocketGuildUser admin)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{user.Username} has been kicked by {admin.Username}#{admin.DiscriminatorValue}"),
                Description = ($"Yay...It's mine now!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media2.giphy.com/media/3oEjI0HA4wxi88yUrC/giphy.gif?cid=3640f6095c23ead37155574263ebbeec")
            };
            e.AddField("Reason:", $"{reason}", false);
            return e;
        }
        /// <summary>
        /// Log: AVB-08 All the users in X role have been kicked.
        /// </summary>
        public static EmbedBuilder avb08(SocketRole role, string reason,SocketGuildUser admin, uint count)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{count} Users from the {role.Name} role have been kicked by {admin.Username}#{admin.DiscriminatorValue}"),
                Description = ($"Ouch. That has to hurt!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media2.giphy.com/media/3oEjI0HA4wxi88yUrC/giphy.gif?cid=3640f6095c23ead37155574263ebbeec")
            };
            e.AddField("Reason:", $"{reason}", false);
            return e;
        }
        /// <summary>
        /// Log: AVB-09 Autorole has been set.
        /// </summary>
        public static EmbedBuilder avb09(SocketRole role)
        {
            var e = new EmbedBuilder()
            {
                Title = ($"{role.Name} has been set as the default role for this server."),
                Description = ($"Now we are getting somewhere!"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("https://media3.giphy.com/media/3o7TKRnkNEjVAMFaso/giphy.gif?cid=3640f6095c2807a36e79654d32d39a98")
            };
            return e;
        }
        /// <summary>
        /// Log: AVB-10 Help Command has been issued.
        /// </summary>
        public static EmbedBuilder avb10(SocketGuild guild)
        {
            var x = ServerList.getServer(guild);
            List<SocketTextChannel> chans = new List<SocketTextChannel>();
            SocketRole ExampleRole = null;
            var e = new EmbedBuilder()
            {
                Title = ($"Commands:"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
            };
            foreach (SocketTextChannel Channels in guild.TextChannels)
            {
                chans.Add(Channels);
            }
            Random Rand = new Random();
            var chan1 = Rand.Next(chans.Count);
            var exampleText = chans[chan1];
            foreach (SocketRole role in guild.Roles)
            {
                if (guild.Roles.Count == 0)
                {
                    ExampleRole = null;
                }
                else
                {
                    ExampleRole = role;
                }
            }
            e.AddField("Server Owner Commands", "\u200b", false);
            if (ExampleRole == null)
            {
                e.AddField("autorole", $"Set the default role upon entering the server.\nUsage: {x.ServerPrefix}autorole @role", true);
                e.AddField("permission", $"Give a user or a role permission to use a certain command.\nUsage: {x.ServerPrefix}permission permission @user // {x.ServerPrefix}permission permission @role", true);
            }
            else
            {
                e.AddField("autorole", $"Set the default role upon entering the server.\nUsage: {x.ServerPrefix}autorole @role\nExample: {x.ServerPrefix}autorole {ExampleRole.Mention}", true);
                e.AddField("permission", $"Give a user or a role permission to use a certain command.\nUsage: {x.ServerPrefix}permission permission @user // {x.ServerPrefix}permission permission @role\nExample: {x.ServerPrefix}permission -ban {guild.Owner.Mention} // {x.ServerPrefix}permission -ban {ExampleRole.Mention}", true);
            }
            e.AddField("logchannel", $"Set the log channel for this server.\nUsage: {x.ServerPrefix}logchannel #channel\nExample: {x.ServerPrefix}logchannel {exampleText.Mention}", true);
            e.AddField("prefix", $"Set the prefix used to activate the bot on this server.\nUsage: {x.ServerPrefix} prefix\nExample: {x.ServerPrefix}prefix %", true);
            return e;
        }
        /// <summary>
        /// Log: AVB-11 Help Command has been issued (Owner Commands Only).
        /// </summary>
        public static EmbedBuilder avb11(SocketGuild guild)
        {
            var x = ServerList.getServer(guild);
            List<SocketTextChannel> chans = new List<SocketTextChannel>();
            SocketRole ExampleRole = null;
            var e = new EmbedBuilder()
            {
                Title = ($"Commands:"),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
            };
            foreach (SocketTextChannel Channels in guild.TextChannels)
            {
                chans.Add(Channels);
            }
            Random Rand = new Random();
            var chan1 = Rand.Next(chans.Count);
            var exampleText = chans[chan1];
            foreach (SocketRole role in guild.Roles)
            {
                if (guild.Roles.Count == 0)
                {
                    ExampleRole = null;
                }
                else
                {
                    ExampleRole = role;
                }
            }
            e.AddField("Server Owner Commands", "\u200b", false);
            if (ExampleRole == null)
            {
                e.AddField("autorole", $"Set the default role upon entering the server.\nUsage: {x.ServerPrefix}autorole @role", true);
                e.AddField("permission", $"Give a user or a role permission to use a certain command.\nUsage: {x.ServerPrefix}permission permission @user // {x.ServerPrefix}permission permission @role", true);
            }
            else
            {
                e.AddField("autorole", $"Set the default role upon entering the server.\nUsage: {x.ServerPrefix}autorole @role\nExample: {x.ServerPrefix}autorole {ExampleRole.Mention}", true);
                e.AddField("permission", $"Give a user or a role permission to use a certain command.\nUsage: {x.ServerPrefix}permission permission @user // {x.ServerPrefix}permission permission @role\nExample: {x.ServerPrefix}permission -ban {guild.Owner.Mention} // {x.ServerPrefix}permission -ban {ExampleRole.Mention}", true);
            }
            e.AddField("logchannel", $"Set the log channel for this server.\nUsage: {x.ServerPrefix}logchannel #channel\nExample: {x.ServerPrefix}logchannel {exampleText.Mention}", true);
            e.AddField("prefix", $"Set the prefix used to activate the bot on this server.\nUsage: {x.ServerPrefix} prefix\nExample: {x.ServerPrefix}prefix %", true);
            return e;
        }
    }
    public class Error : ModuleBase<SocketCommandContext>
    {
        //// Autonomous Virtual Builders \\\\
        /// <summary>
        /// Error: AVB-01 No Log Channel defined.
        /// </summary>
        public static EmbedBuilder avb01(SocketGuild guild)
        {
            var x = ServerList.getServer(guild);
            var e = new EmbedBuilder()
            {
                Title = ($"No log channel has been defined."),
                Description = ($"Please use {x.ServerPrefix}help logs to find out how to set a log channel."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            ServerList.SaveServer();
            return e;
        }
        /// <summary>
        /// Error: AVB-02 No User Specified.
        /// </summary>
        public static EmbedBuilder avb02(SocketGuild guild)
        {
            var x = ServerList.getServer(guild);
            var e = new EmbedBuilder()
            {
                Title = ($"No User has been specified."),
                Description = ($"Please use {x.ServerPrefix}help `command` to find out how to use this command."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            ServerList.SaveServer();
            return e;
        }
        /// <summary>
        /// Error: AVB-03 Incorrect Permissions.
        /// </summary>
        public static EmbedBuilder avb03()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"You do not have the neccesary permissions."),
                Description = ($"If you believe this is an issue, please contact the server owner."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
        /// <summary>
        /// Error: AVB-04 Ban Reason Not Set.
        /// </summary>
        public static EmbedBuilder avb04()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"You did not specify a reason."),
                Description = ($"Please reissue the command while specifying a reason."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
        /// <summary>
        /// Error: AVB-05 No prefix was set.
        /// </summary>
        public static EmbedBuilder avb05()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"You did not specify a prefix."),
                Description = ($"Please reissue the command while specifying a prefix."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
        /// <summary>
        /// Error: AVB-06 No permission was specified.
        /// </summary>
        public static EmbedBuilder avb06()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"You did not specify a permission."),
                Description = ($"Please reissue the command while specifying a permission."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
        /// <summary>
         /// Error: AVB-07 No role Specified.
         /// </summary>
        public static EmbedBuilder avb07(SocketGuild guild)
        {
            var x = ServerList.getServer(guild);
            var e = new EmbedBuilder()
            {
                Title = ($"No role has been specified."),
                Description = ($"Please use {x.ServerPrefix}help `command` to find out how to use this command."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            ServerList.SaveServer();
            return e;
        }
        /// <summary>
        /// Error: AVB-08 No members in specified role.
        /// </summary>
        public static EmbedBuilder avb08()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"This role has no members."),
                Description = ($"Please choose a different role that has members."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
        /// <summary>
        /// Error: AVB-09 Couldn't find specified role.
        /// </summary>
        public static EmbedBuilder avb09()
        {
            var e = new EmbedBuilder()
            {
                Title = ($"I could not set a user to the autorole."),
                Description = ($"It seems the specified role has been deleted."),
                Timestamp = (DateTime.Now),
                Color = new Color(142, 5, 5),
                ThumbnailUrl = ("http://www.free-icons-download.net/images/red-error-flag-icon-41893.png")
            };
            return e;
        }
    }
    public class HelpItem : ModuleBase<SocketCommandContext>
    {

    }
}