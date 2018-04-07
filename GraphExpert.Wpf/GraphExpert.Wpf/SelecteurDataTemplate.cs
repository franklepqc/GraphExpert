using GraphExpert.Wpf.Models;
using System.Windows;
using System.Windows.Controls;

namespace GraphExpert.Wpf
{
    public class SelecteurDataTemplate : DataTemplateSelector
    {
        public DataTemplate TemplateArret { get; set; }

        public DataTemplate TemplateLiaison { get; set; }

        public DataTemplate TemplateAgent { get; set; }

        public DataTemplate TemplatePort { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item?.GetType() == typeof(StopVM))
            {
                return TemplateArret;
            }
            else if (item?.GetType() == typeof(LineVM))
            {
                return TemplateLiaison;
            }
            else if (item?.GetType() == typeof(AgentVM))
            {
                return TemplateAgent;
            }
            else if (item?.GetType() == typeof(PortVM))
            {
                return TemplatePort;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
