namespace NoLimitTech.Application
{
    public static class AppConstants
    {
        // Web Sockets group
        /// <summary>Template: EVENT-GROUP-{EVENT ID}</summary>
        public const string WEBSOCKET_EVENT_GROUP_ = "EVENT-GROUP-";
        /// <summary>Template: FLOOR-GROUP-{FLOOR ID}</summary>
        public const string WEBSOCKET_FLOOR_GROUP_ = "FLOOR-GROUP-";
        /// <summary>Template: DESK-GROUP-{DESK ID}</summary>
        public const string WEBSOCKET_DESK_GROUP_ = "DESK-GROUP-";
        /// <summary>Template: PRESENTATION-GROUP-{PRESENTATION ID}</summary>
        public const string WEBSOCKET_PRESENTATION_GROUP_ = "PRESENTATION-GROUP-";

        // UNITS
        public const string PRICE_UNITS = "$";
        public const string TRIAL_PERIOD_UNITS = "month";
        public const string DURATION_UNITS = "hours";
        public const string HD_RECORDING_UNITS = "GB";
        

        // JWT CLAIMS
        public const string IS_REGISTERED = "registered";
        public const string BY_INVITE = "by-invite";
    }
}
