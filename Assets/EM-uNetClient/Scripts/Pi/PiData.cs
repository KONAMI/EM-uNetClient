using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using Utf8Json;

namespace kde.tech
{

public class PiData
{

    [Serializable]
    public class NetworkParams {
	[DataMember(Name = "bandUp")]	
	public int bandUp = 8096;
	[DataMember(Name = "bandDw")]	
	public int bandDw = 8096;
	[DataMember(Name = "delayUp")]	
	public int delayUp = 0;
	[DataMember(Name = "delayDw")]	
	public int delayDw = 0;
	[DataMember(Name = "lossUp")]	
	public int lossUp = 0;
	[DataMember(Name = "lossDw")]	
	public int lossDw = 0;
	[DataMember(Name = "disconnUp")]	
	public int disconnUp = 0;
	[DataMember(Name = "disconnDw")]	
	public int disconnDw = 0;
    }
}
}
