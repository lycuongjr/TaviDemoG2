using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Tavi.Demo.G2.HtmlEx
{
  public static  class HtmlExtension
    {
        #region Button
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonText)
        {
            return Button(htmlHelper, buttonText, "btn btn-default");
        }
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonText, string className)
        {
            return new MvcHtmlString(string.Format("<input type=\"button\" value=\"{0}\" class=\"{1}\"/>", buttonText, className));
        }
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonText, string className, string buttonId)
        {
            return new MvcHtmlString(string.Format("<input type=\"button\" value=\"{0}\" id=\"{1}\" class=\"{2}\"/>", buttonText, buttonId, className));
        }
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonText, string className, string buttonId, string icon)
        {
            return new MvcHtmlString(string.Format("<div  id=\"{1}\" class=\"{2}\"><i class=\"{3}\"></i> {0}</div>", buttonText, buttonId, className, icon));
        }

        #endregion button
        #region html checkbox extension

        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, bool isDisabled)
        {
            var attributes = new Dictionary<string, string>();
            attributes.Add("class", "minimal");
            if (isDisabled)
            {
                attributes.Add("disabled", "disabled");
            }
            return htmlHelper.CheckBox(name, isChecked);
        }
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, bool isDisabled, IDictionary<string, object> htmlAttributes)
        {
            htmlAttributes.Add("class", "minimal");
            if (isDisabled)
            {
                htmlAttributes.Add("disabled", "disabled");
            }
            return htmlHelper.CheckBox(name, isChecked, htmlAttributes);
        }
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, bool isDisabled, object htmlAttributes)
        {
            var attributes = new Dictionary<string, object>();
            attributes.Add("class", "minimal");
            if (isDisabled)
            {

                attributes.Add("disabled", "disabled");
                int s = htmlAttributes.GetType().GetProperties().Count();
                foreach (PropertyInfo property in htmlAttributes.GetType().GetProperties())
                {
                    object propertyValue = property.GetValue(htmlAttributes, null);
                    attributes.Add(property.Name, propertyValue);
                }
                return htmlHelper.CheckBox(name, isChecked, attributes);
            }
            else
            {
                return htmlHelper.CheckBox(name, isChecked, htmlAttributes);
            }
        }
        public static MvcHtmlString htmlCheckBox(this HtmlHelper htmlHelper, string name)
        {
            string html = "<input type='checkbox' class='minimal' name=" + name + " id=" + name + ">";

            return new MvcHtmlString(html);
        }
        public static MvcHtmlString htmlCheckBox(this HtmlHelper htmlHelper, string name, object val)
        {
            string html = "<input type='checkbox' class='minimal' name=" + name + " id=" + name + " value=" + val + ">";

            return new MvcHtmlString(html);
        }
        public static MvcHtmlString htmlCheckBox(this HtmlHelper htmlHelper, string name, object val, bool? isChecked)
        {
            string strCheck = isChecked == true ? " checked" : string.Empty;
            string html = "<input type='checkbox' class='minimal' name=" + name + " id=" + name + " value='" + val + "'" + strCheck + ">";

            return new MvcHtmlString(html);
        }
        #endregion html checkbox extension
    }
}
