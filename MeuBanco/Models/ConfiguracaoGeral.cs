using ApiService;

namespace MonitorCompre.models
{
    public class ConfiguracaoGeral : ConfSync
    {
        public decimal TempoResync { get; set; }
        /*public string LinkApi { get; set; }*/

        public ConfiguracaoGeral()
        {

        }
    }
}