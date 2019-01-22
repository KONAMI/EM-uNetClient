using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace kde.tech
{
    
public abstract class FuncObjBase : ScriptableObject
{
    [Serializable]
    public class ListNodeBase {
	public string label;
    }
    
    public abstract Task Invoke();

    protected Stopwatch m_sw = new System.Diagnostics.Stopwatch();
    protected long m_lastBench = 0;
    protected void BenchStart(){
	m_sw.Start();
    }
    protected void BenchEnd(){
	m_sw.Stop();
	m_lastBench = m_sw.ElapsedMilliseconds;
	m_sw.Reset();
    }

    
}
}
