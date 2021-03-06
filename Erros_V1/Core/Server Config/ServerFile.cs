﻿using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Erros
{
    public class ServerFile
    {
        public ulong ServerID { get; set; } // Server Unique ID
        public string ServerName { get; set; } // Server Name
        public string ServerOwner { get; set; } // The Owner of the server
        public ulong ServerOwnerID { get; set; } // The Owner of the servers Unique ID
        public string ServerLogChannel { get; set; } // The Channel set for outputting logs
        public string ServerPrefix { get; set; } // The servers Preferred Prefix defaulted to ')'
        public string AutoRoleName { get; set; } // The servers default role upon joining the server
        public ulong AutoRoleID { get; set; } // The servers default role ID to be given upon joining the server
        public List<ulong> BanUserIDs { get; set; } // Any USERS with access to the ban command
        public List<ulong> BanRoleIDs { get; set; } // Any ROLES with access to the ban command
        public List<ulong> KickUserIDs { get; set; } // Any USERS with access to the kick command
        public List<ulong> KickRoleIDs { get; set; } // Any ROLES with access to the kick command
        public List<ulong> MuteUserIDs { get; set; } // Any USERS with access to the mute command
        public List<ulong> MuteRoleIDs { get; set; } // Any ROLES with access to the mute command
        public List<ulong> blackListUserIDs { get; set; } // Any USERS with access to the blacklist command
        public List<ulong> blackListRoleIDs { get; set; } // Any ROLES with access to the blacklist command
        public List<ulong> BlackListedUsers { get; set; } // Users Messages will be ignored
        public List<ulong> MutedUserIDs { get; set; } // Users Messages will be deleted
        public List<ulong> TimeoutUsersID { get; set; } // Users who are currently 'Timed Out' Unique ID
        public List<ulong> TimeoutLength { get; set; } // The corresponding length of time to 'Time Out' a user (In Minutes)
        public List<string> TimeoutReason { get; set; } // The corresponding reason for a 'Time Out'
        public List<ulong> PruneUserIDs { get; set; } // Any USERS with access to the prune/purge command
        public List<ulong> PruneRoleIDs { get; set; } // Any ROLES with access to the prune/purge command
        public List<ulong> TeamVCs { get; set; } // Any 'Team' Voice Channels currently setup for Cross-play
        public List<ulong> TeamVCOwnerID { get; set; } // The 'Team' Voice Channel Owner, If they leave, the voice channel is deleted.
    }
}