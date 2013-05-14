using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Boomerang.Web.Providers
{
    public static class AddressProvider
    {
        public static string AddAddressForDisplay(bool newRow)
        {
            var sb = new StringBuilder();

            if (newRow)
                sb.Append("<div class=\"table-row\">" + BuildAddressDisplay(new Address(), false) + "</div>");
            else
                sb.Append(BuildAddressDisplay(new Address(), true));

            return sb.ToString();
        }

        public static string DisplayAddresses(Collection<Address> addresses)
        {
            var counter = (addresses != null) ? addresses.Count : 0;
            var sb = new StringBuilder();

            if (counter > 0)
            {
                for (int i = 0; i < counter; i++)
                {
                    var mod = i % 2;
                    var isLast = (mod == 0) ? false : true;

                    if (mod == 0)
                        sb.Append("<div class=\"table-row\">" + BuildAddressDisplay(addresses[i], false));
                    else
                        sb.Append(BuildAddressDisplay(addresses[i], true) + "</div>");
                }

                if (counter % 2 == 1)
                    sb.Append("</div>");
            }
            else
                sb.Append("<div class=\"table-row\">" + BuildAddressDisplay(new Address(), false) + "</div>");

            return sb.ToString();
        }

        private static string BuildAddressDisplay(Address address, bool isLast)
        {
            var sb = new StringBuilder();
            var last = (isLast) ? "last" : "";
            var checkInvoice = (address.Invoice) ? " checked=\"checked\"" : "";
            var postCode = (address.PostCode != null) ? address.PostCode.ToUpper() : "";

            sb.Append("<div class=\"table-cell sixcol " + last + "\">" +
                        "<fieldset>" +
                            "<legend>Address</legend>" +
                                "<div class=\"table address\" id=\"" + address.Id + "\">" +
                                    "<div class=\"table-row\">" +
                                        "<div class=\"table-cell fourcol\">" +
                                            "<label>Address 1</label>" +
                                        "</div>" +
                                        "<div class=\"table-cell eightcol last\">" +
                                            "<div class=\"input\"><input type=\"text\" name=\"HouseNumber\" value=\"" + address.HouseNumber + "\" tabindex=\"2\" /></div>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div class=\"table-row\">" +
                                        "<div class=\"table-cell fourcol\">" +
                                            "<label>Address 2</label>" +
                                        "</div>" +
                                        "<div class=\"table-cell eightcol last\">" +
                                            "<div class=\"input\"><input type=\"text\" name=\"Street\" value=\"" + address.Street + "\" tabindex=\"3\" /></div>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div class=\"table-row\">" +
                                        "<div class=\"table-cell fourcol\">" +
                                            "<label>Address 3</label>" +
                                        "</div>" +
                                        "<div class=\"table-cell eightcol last\">" +
                                            "<div class=\"input\"><input type=\"text\" name=\"Area\" value=\"" + address.Area + "\" tabindex=\"4\" /></div>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div class=\"table-row\">" +
                                        "<div class=\"table-cell fourcol\">" +
                                            "<label>Town/City</label>" +
                                        "</div>" +
                                    "<div class=\"table-cell eightcol last\">" +
                                        "<div class=\"input\"><input type=\"text\" name=\"Town\" value=\"" + address.Town + "\" tabindex=\"5\" /></div>" +
                                    "</div>" +
                                "</div>" +
                                "<div class=\"table-row\">" +
                                    "<div class=\"table-cell fourcol\">" +
                                        "<label>County</label>" +
                                    "</div>" +
                                "<div class=\"table-cell eightcol last\">" +
                                    DisplayCountyDropDown(address.GetCountyList(), address.County, 6) +
                                "</div>" +
                            "</div>" +
                            "<div class=\"table-row\">" +
                                "<div class=\"table-cell fourcol\">" +
                                    "<label>Post Code</label>" +
                                "</div>" +
                                "<div class=\"table-cell eightcol last\">" +
                                "<div class=\"input\"><input type=\"text\" name=\"PostCode\" id=\"PostCode\" value=\"" + postCode + "\" tabindex=\"7\" /></div>" +
                                "</div>" +
                            "</div>" +
                            "<div class=\"table-row\">" +
                                "<div class=\"table-cell fourcol\">" +
                                    "<label>Invoice address</label>" +
                                "</div>" +
                                "<div class=\"table-cell eightcol last\">" +
                                "<div class=\"radio\"><input type=\"checkbox\" name=\"Invoice\" id=\"Invoice\"" + checkInvoice + " tabindex=\"8\" /></div>" +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                        "<div class=\"no-border\">" +
                            "<button class=\"btnAddAddress\">Add more</button>" +
                            "<button class=\"removeAddress\" data-id=\"" + address.Id + "\">Remove</button>" +
                        "</div>" +
                    "</fieldset>" +
                "</div>");

            return sb.ToString();
        }

        public static string DisplayCountyDropDown(Dictionary<string, string> countyList, string selectedCounty, int tabIndex)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(selectedCounty))
                selectedCounty = "West Yorkshire";

            sb.Append("<select name=\"County\" tabindex=\"" + tabIndex + "\">");
            foreach (KeyValuePair<string, string> pair in countyList)
            {
                var selected = (pair.Value == selectedCounty) ? "selected=\"selected\"" : "";

                sb.Append("<option " + selected + " value=\"" + pair.Key + "\">" + pair.Value + "</option>");
            }
            sb.Append("</select>");

            return sb.ToString();
        }
    }
}
