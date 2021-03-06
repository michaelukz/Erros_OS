﻿using Config;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Erros;
using System.Timers;
using System.Threading.Tasks;
using Erros.Errors;
using Timer = System.Timers.Timer;

namespace HandleCommands
{
    /// <summary> Detect whether a message is a command, then execute it. </summary>

    public class CommandHandler : InteractiveBase
    {
        public DiscordSocketClient _client;
        public static Timer v1;
        public static SocketGuildUser LastUser;
        public static DateTime OldDay;
        public static uint CommandsToday;
        public static uint LinksToday;
        private CommandService _cmds;
        internal async Task InstallAsync(DiscordSocketClient c)
        {
            _client = c;                                                 // Save an instance of the discord client.
            _cmds = new CommandService();                                // Create a new instance of the commandservice. 
            _client.MessageReceived += HandleCommandAsync;
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());    // Load all modules from the assembly.
            SocketSelfUser user = _client.CurrentUser;
            _client.Ready += ClientReady;
            _client.UserJoined += UserJoinedGuild;
            _client.JoinedGuild += FetchUsers;
            _client.UserVoiceStateUpdated += UserVoiceChannelState;
        }

        private async Task UserVoiceChannelState(SocketUser user, SocketVoiceState old, SocketVoiceState newv)
        {
            var x = ServerList.getServer(old.VoiceChannel.Guild);
            Predicate<ulong> pred = (ulong p) => { return p == old.VoiceChannel.Id; };
            var l = x.TeamVCs.FindIndex(pred);
            var vcOwner = x.TeamVCOwnerID.ElementAt(l);

            if (x.TeamVCs.Contains(old.VoiceChannel.Id))
            {
                if (user.Id == vcOwner)
                {
                    var chan = old.VoiceChannel;
                    await chan.DeleteAsync();
                    await user.SendMessageAsync("You left the voice channel you where the owner of, so I have deleted it.");
                    x.TeamVCs.RemoveAt(l);
                    x.TeamVCOwnerID.RemoveAt(l);
                }
            }
            ServerList.SaveServer();
        }

        private async Task FetchUsers(SocketGuild arg)
        {
            var x = ServerList.getServer(arg); // Add the Server to the server list if not already there.
            var users =  arg.Users; // Fetch all the users in the server and store in the 'users' variable.
            foreach (SocketGuildUser user in users) // Create a loop running through all users of the server, sets current to 'user'
            {
                var l = UserList.getUser(user); // Add the user to the user list if not already there.
                UserList.SaveUser(); // Save the userlist.
            }
            ServerList.SaveServer(); // Save the serverlist.
            Console.WriteLine($"Connected to new server [{arg.Name}] and added all users to userlist.");
        }

        private async Task UserJoinedGuild(SocketGuildUser user)
        {
            var g = user.Guild;
            var x = ServerList.getServer(g);
            var u = UserList.getUser(user);
            var output = ulong.TryParse(x.ServerLogChannel, out ulong LogID);
            var log = g.GetTextChannel(LogID);
            if (x.AutoRoleName == "nul")
            {
                Console.WriteLine($"[{g.Name}] A new user has joined. No Autorole is set.");
                return;
            }
            else
            {
                var role = g.GetRole(x.AutoRoleID);
                if (role == null)
                {
                    EmbedBuilder e = Error.avb09();
                    await log.SendMessageAsync("", false, e.Build());
                }
                else
                {
                    await user.AddRoleAsync(role);
                    Console.WriteLine($"[{g.Name}] A new user has joined. Assigned {user.Username} the {role.Name} role.");
                }
            }
            UserList.SaveUser();
            ServerList.SaveServer();
        }

        public static uint GetCommands()
        {
            return CommandsToday;
        }
        private async Task ClientReady()
        {
            await Timeouts.SetContext(_client);
            await _client.SetStatusAsync(UserStatus.Idle);
            await _client.SetGameAsync("Listening for commands!", null, ActivityType.Listening);
            await InstantiateTimer();
        }

        public static Task InstantiateTimer()
        {
            //1500000 = 25Minutes
            v1 = new Timer() { Interval = 1500000, Enabled = true, AutoReset = true };
            v1.Elapsed += V1_Ticked;
            v1.Start();
            OldDay = DateTime.Now.Date;
            return Task.CompletedTask;
        }
        public async static void V1_Ticked(object sender, ElapsedEventArgs e)
        {
            if (OldDay != DateTime.Now.Date)
            {
                Console.WriteLine("A day has just passed. Commands Reset to 0.");
                CommandsToday = 0;
            }
        }
        public async Task RGFXServer(SocketMessage s)
        {
            string msg = s.Content;
            var channel = (SocketTextChannel)s.Channel;
            var x = ServerList.getServer(channel.Guild);
            var l = s as SocketUserMessage;
            var user = (SocketGuildUser)s.Author;
            if (msg.Contains("https://www.gifyourgame.com") || msg.Contains("https://medal.tv/"))
            {
                LinksToday += 1;
                LinkLevelSystem.AddLink(user, s);
            }
            ServerList.SaveServer();
        }
        public async Task UPServer(SocketMessage s)
        {
            string msg = s.Content;
            var channel = (SocketTextChannel)s.Channel;
            var x = ServerList.getServer(channel.Guild);
            var l = s as SocketUserMessage;
            var user = (SocketGuildUser)s.Author;
            var u = UserList.getUser(user);
            if (u.Level == 20)
            {
                UPRankSystem.Member(user);
            }
            else if (u.Level == 30)
            {
                UPRankSystem.Trusted(user);
            }
            UserList.SaveUser();
            ServerList.SaveServer();
        }
        public async Task HandleExperience(SocketMessage s)
        {
            var msg = s;
            var channel = (SocketTextChannel)s.Channel;
            var x = ServerList.getServer(channel.Guild);
            var guser = channel.Guild.GetUser(s.Author.Id);
            var l = s as SocketUserMessage;
            var user = (SocketGuildUser)s.Author;
            if (LastUser == guser)
            {
                Console.WriteLine($"{guser.Username} was the last messager, no experience given.");
                return;
            }
            else
            {
                LastUser = guser;
                var msgcount = msg.MentionedUsers.Count;
                var rolecount = msg.MentionedRoles.Count;
                var chancount = msg.MentionedChannels.Count;
                var toString = msg.Content;
                var xptoadd = 0;
                if (msgcount > 0)
                {
                    foreach (SocketUser users in msg.MentionedUsers)
                    {
                        xptoadd += 1;
                        toString = toString.Replace($"<@{users.Id}>", "");
                    }
                }
                if (rolecount > 0)
                {
                    foreach (SocketRole role in msg.MentionedRoles)
                    {
                        xptoadd += 1;
                        toString = toString.Replace($"<@{role.Id}>", "");
                    }
                }
                if (chancount > 0)
                {
                    foreach (SocketChannel chan in msg.MentionedChannels)
                    {
                        xptoadd += 1;
                        toString = toString.Replace($"<#{chan.Id}>", "");
                    }
                }
                double doublexp = toString.Count() + xptoadd;
                var xp = Math.Round(doublexp, MidpointRounding.ToEven);
                await Levels.Calculator(channel.Guild, (SocketGuildUser)s.Author, (int)xp);
                UserList.SaveUser();
                ServerList.SaveServer();
            }
        }
        public async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;//                     Convert the message to a socketUSERmessage
            var channel = (SocketTextChannel)s.Channel;//           Fetches the channel the message was sent in
            var user = channel.Guild.GetUser(s.Author.Id); //       Instantiates the user as a socketguilduser
            var x = ServerList.getServer(channel.Guild);//          Fetches the server from the server list
            var u = UserList.getUser(user); //                      Fetches the user from the user list
            var services = new ServiceCollection() //               Creates Interactive Services
                 .AddSingleton(_client)
                 .AddSingleton<InteractiveService>()
                 .BuildServiceProvider(); 
            var context = new SocketCommandContext(_client, msg);     // Create a new command context
            if (context.IsPrivate) { return; } // If it is a private message, ignore it.
            var app = await context.Client.GetApplicationInfoAsync(); // Gets application information
            if (s.Author.IsBot) return; //                            If the Messager is a bot Ignore it
            if (channel.Guild.Id == 521005903034712076) { await RGFXServer(s); } // RGFX Channel ID: 521005903034712076
            await HandleExperience(msg);
            Console.WriteLine($"[{s.Channel}][{s.Author}]: ({s}) @{DateTime.Now.Hour}:{DateTime.Now.Minute} "); // Console Logging Messages
            if (x.MutedUserIDs.Contains(s.Author.Id)) { await s.DeleteAsync(); Console.WriteLine("User is Muted"); return; } // If the user is chat muted delete message
            if (x.BlackListedUsers.Contains(s.Author.Id)) { Console.WriteLine("User is Blacklisted"); return; } // if the user is blacklisted from using the bot, ignore him
            u.Messages += 1;//                                        Add 1 message to the users MessageCount
            int argPos = 0;                                           // Check if the message has either a string or mention prefix
            if (msg.HasStringPrefix(x.ServerPrefix, ref argPos) || 
                msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {                                                         // Try and execute a command with the given context
                var result = await _cmds.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)                                // If execution failed return
                    return;
                else
                {
                    CommandsToday += 1;//                            Add 1 command to the command count of the day
                }
            }
            UserList.SaveUser();
            ServerList.SaveServer();
        }
    }
}