using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognizer
{
    public class SpeechCommandRecognizerOptions
    {
        public string SubscriptionKey { get; set; } = string.Empty;
        public string ServiceRegion { get; set; } = string.Empty;        
    }
}
