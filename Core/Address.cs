using Core.Data;
using System.Collections.Generic;

namespace Core
{
    public class Address
    {

        #region Fields

        private Dictionary<string, string> m_county = new Dictionary<string, string>()
        {
            { "", "Please select" },
            { "Avon", "Avon" },
            { "Bedfordshire", "Bedfordshire" },
            { "Berkshire", "Berkshire" },
            { "Bristol", "Bristol" },
            { "Buckinghamshire", "Buckinghamshire" },
            { "Cambridgeshire", "Cambridgeshire" },
            { "Cheshire", "Cheshire" },
            { "Cleveland", "Cleveland" },
            { "Cornwall", "Cornwall" },
            { "Cumbria", "Cumbria" },
            { "Derbyshire", "Derbyshire" },
            { "Devon", "Devon" },
            { "Dorset", "Dorset" },
            { "Durham", "Durham" },
            { "East Riding of Yorkshire", "East Riding of Yorkshire" },
            { "East Sussex", "East Sussex" },
            { "Essex", "Essex" },
            { "Gloucestershire", "Gloucestershire" },
            { "Greater Manchester", "Greater Manchester" },
            { "Hampshire", "Hampshire" },
            { "Herefordshire", "Herefordshire" },
            { "Hertfordshire", "Hertfordshire" },
            { "Humberside", "Humberside" },
            { "Isle of Wight", "Isle of Wight" },
            { "Isles of Scilly", "Isles of Scilly" },
            { "Kent", "Kent" },
            { "Lancashire", "Lancashire" },
            { "Leicestershire", "Leicestershire" },
            { "Lincolnshire", "Lincolnshire" },
            { "London", "London" },
            { "Merseyside", "Merseyside" },
            { "Middlesex", "Middlesex" },
            { "Norfolk", "Norfolk" },
            { "North Yorkshire", "North Yorkshire" },
            { "Northamptonshire", "Northamptonshire" },
            { "Northumberland", "Northumberland" },
            { "Nottinghamshire", "Nottinghamshire" },
            { "Oxfordshire", "Oxfordshire" },
            { "Rutland", "Rutland" },
            { "Shropshire", "Shropshire" },
            { "Somerset", "Somerset" },
            { "South Yorkshire", "South Yorkshire" },
            { "Staffordshire", "Staffordshire" },
            { "Suffolk", "Suffolk" },
            { "Surrey", "Surrey" },
            { "Tyne and Wear", "Tyne and Wear" },
            { "Warwickshire", "Warwickshire" },
            { "West Midlands", "West Midlands" },
            { "West Sussex", "West Sussex" },
            { "West Yorkshire", "West Yorkshire" },
            { "Wiltshire", "Wiltshire" },
            { "Worcestershire", "Worcestershire" },
            { "--UK Offshore--", "--UK Offshore--" },
            { "Channel Islands", "Channel Islands" },
            { "Isle of Man", "Isle of Man" },
            { "--Northern Ireland--", "--Northern Ireland--" },
            { "Antrim", "Antrim" },
            { "Armagh", "Armagh" },
            { "Down", "Down" },
            { "Fermanagh", "Fermanagh" },
            { "Londonderry", "Londonderry" },
            { "Tyrone", "Tyrone" },
            { "--Scotland--", "--Scotland--" },
            { "Aberdeen City", "Aberdeen City" },
            { "Aberdeenshire", "Aberdeenshire" },
            { "Angus", "Angus" },
            { "Argyll and Bute", "Argyll and Bute" },
            { "Borders", "Borders" },
            { "Clackmannan", "Clackmannan" },
            { "Dumfries and Galloway", "Dumfries and Galloway" },
            { "Dundee (City of)", "Dundee (City of)" },
            { "East Ayrshire", "East Ayrshire" },
            { "East Dunbartonshire", "East Dunbartonshire" },
            { "East Lothian", "East Lothian" },
            { "East Renfrewshire", "East Renfrewshire" },
            { "Edinburgh (City of)", "Edinburgh (City of)" },
            { "Falkirk", "Falkirk" },
            { "Fife", "Fife" },
            { "Glasgow (City of)", "Glasgow (City of)" },
            { "Highland", "Highland" },
            { "Inverclyde", "Inverclyde" },
            { "Midlothian", "Midlothian" },
            { "Moray", "Moray" },
            { "North Ayrshire", "North Ayrshire" },
            { "North Lanarkshire", "North Lanarkshire" },
            { "Orkney", "Orkney" },
            { "Perthshire and Kinross", "Perthshire and Kinross" },
            { "Renfrewshire", "Renfrewshire" },
            { "Shetland", "Shetland" },
            { "South Ayrshire", "South Ayrshire" },
            { "South Lanarkshire", "South Lanarkshire" },
            { "Stirling", "Stirling" },
            { "West Dunbartonshire", "West Dunbartonshire" },
            { "West Lothian", "West Lothian" },
            { "Western Isles", "Western Isles" },
            { "--Wales--", "--Wales--" },
            { "Blaenau Gwent", "Blaenau Gwent" },
            { "Bridgend", "Bridgend" },
            { "Caerphilly", "Caerphilly" },
            { "Cardiff", "Cardiff" },
            { "Carmarthenshire", "Carmarthenshire" },
            { "Ceredigion", "Ceredigion" },
            { "Conwy", "Conwy" },
            { "Denbighshire", "Denbighshire" },
            { "Flintshire", "Flintshire" },
            { "Gwynedd", "Gwynedd" },
            { "Isle of Anglesey", "Isle of Anglesey" },
            { "Merthyr Tydfil", "Merthyr Tydfil" },
            { "Monmouthshire", "Monmouthshire" },
            { "Neath Port Talbot", "Neath Port Talbot" },
            { "Newport", "Newport" },
            { "Pembrokeshire", "Pembrokeshire" },
            { "Powys", "Powys" },
            { "Rhondda Cynon Taff", "Rhondda Cynon Taff" },
            { "Swansea", "Swansea" },
            { "Torfaen", "Torfaen" },
            { "The Vale of Glamorgan", "The Vale of Glamorgan" },
            { "Wrexham", "Wrexham" }
        };

        #endregion

        #region Properties

        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CandidateId { get; set; }
        
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

        #endregion

        #region Constructors

        public Address()
        {
        }

        public Address(int id)
        {
            Address address = Addresses.Get(id);
            Id = address.Id;
            ClientId = address.ClientId;
            CandidateId = address.CandidateId;

            HouseNumber = address.HouseNumber;
            Street = address.Street;
            Area = address.Area;
            Town = address.Town;
            County = address.County;
            PostCode = address.PostCode;
        }

        #endregion

        #region Public methods

        public Dictionary<string, string> GetCountyList()
        {
            return m_county;
        }

        public void Save()
        {
            if (this.Id > 0)
                Addresses.Edit(this);
            else
                this.Id = Addresses.Create(this);
        }

        public void Delete()
        {
            if (this.Id > 0)
                Addresses.Delete(this.Id);
        }

        #endregion

    }
}
