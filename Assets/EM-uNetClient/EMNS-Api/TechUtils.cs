using System;

namespace kde.tech
{
public static class AmUtils
{
    private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long timestamp {
	get {
	    return (FromDateTime( DateTime.UtcNow ));
	}
    }

    public static DateTime ToLocalDateTime(this long unixTime){
	return UNIX_EPOCH.AddSeconds(unixTime).ToLocalTime();
    }

    public static DateTime ToJpDateTime(this long unixTime){
	//var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
	return UNIX_EPOCH.AddSeconds(unixTime).AddMinutes(60*9);	
    }
        
    public static DateTime ToDateTime(this long unixTime){
	return UNIX_EPOCH.AddSeconds(unixTime);
    }
    
    public static long FromDateTime(DateTime dateTime){
	double nowTicks = ( dateTime.ToUniversalTime() - UNIX_EPOCH ).TotalSeconds;
	return (long)nowTicks;
    }

    public static DateTime ToLocalDateTime(this DateTime dateTime){
	TimeZoneInfo tzi = TimeZoneInfo.Local;
	DateTime local = TimeZoneInfo.ConvertTimeFromUtc(dateTime, tzi);
	return local;
    }

    public static DateTime ToJpDateTime(this DateTime dateTime){
	//var jstTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
	return dateTime.AddMinutes(60*9);
    }
    
}
}
