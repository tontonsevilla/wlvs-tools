using HtmlAgilityPack;
using System.Globalization;
using System.Web;
using WLVSTools.Web.Models.DeveloperTools;
using WLVSTools.Web.Models.Generate;
using WLVSTools.Web.Services;

namespace WLVSTools.Web.ApplicationServices
{
    public class GenerateApplicationViewService
    {
        private readonly GeneratorService _generatorService;

        public GenerateApplicationViewService()
        {
            _generatorService = new GeneratorService();
        }

        public Personalnfo PersonalInfo(GenerateFakePersonalInfo data)
        {
            var countryCode = getFakeNameGeneratorCountry(data.Country);
            var htmlString = _generatorService.GeneratePersonalInfo(countryCode, data.State);
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(htmlString);

            var nodes = htmlDoc.DocumentNode.Descendants(0).Where(n => n.HasClass("content"));
            var basicInformatioNode = nodes.Where(n => n.InnerText.Contains("Basic information")).FirstOrDefault();
            HtmlNode tableBasicInformation = basicInformatioNode.ChildNodes[1].ChildNodes[0];

            if (!string.IsNullOrWhiteSpace(data.State))
            {
                tableBasicInformation = basicInformatioNode.ChildNodes[2].ChildNodes[0];
            }

            var fullName = tableBasicInformation.ChildNodes[1].ChildNodes[1].InnerText.Replace("&nbsp;", " ");
            var gender = tableBasicInformation.ChildNodes[4].ChildNodes[1].InnerText;
            var email = normalizedCharacters($"tonton.blast+{fullName.Replace(" ", "")}@gmail.com");
            var sevisCountry = getSevisCountry(data.Country);

            if (countryCode != "uk-")
            {
                data.State = tableBasicInformation.ChildNodes.Where(n =>
                                                    n.InnerText.IndexOf("State(area/province)", System.StringComparison.OrdinalIgnoreCase) >= 0)
                .First().ChildNodes[1].SelectNodes("small")[0].InnerText.Split(' ')[1].Replace(")", "");
            }

            var address = new Address
            {
                Address1 = tableBasicInformation.ChildNodes.Where(n =>
                                                     n.InnerText.IndexOf("Street address", System.StringComparison.OrdinalIgnoreCase) >= 0)
                .First().ChildNodes[1].InnerText.Replace("&nbsp;", " "),

                City = tableBasicInformation.ChildNodes.Where(n =>
                                                 n.InnerText.IndexOf("City", System.StringComparison.OrdinalIgnoreCase) >= 0)
                .First().ChildNodes[1].InnerText.Replace("&nbsp;", " "),

                State = data.State,

                PostalCode = tableBasicInformation.ChildNodes.Where(n =>
                                                       n.InnerText.IndexOf("Zipcode", System.StringComparison.OrdinalIgnoreCase) >= 0)
                .First().ChildNodes[1].InnerText.Replace("&nbsp;", " "),

                Country = string.IsNullOrWhiteSpace(countryCode) ? "US" : countryCode.Replace("-", "").ToUpper(),

                SevisCountry = string.IsNullOrWhiteSpace(sevisCountry) ? "US" : sevisCountry.ToUpper()
            };

            var company = Company();

            var personData = new Personalnfo
            {
                FirstName = $"{fullName.Split(' ')[0]} Test Data",
                LastName = fullName.Split(' ')[1],
                Gender = gender,
                Email = email,
                Address = address,
                UserInfo = new UserInfo
                {
                    UserName = email,
                    Password = "P@ssw0rd"
                },
                Company = company
            };

            return personData;
        }

        private static string normalizedCharacters(string value)
        {
            var decomposed = value.Normalize(System.Text.NormalizationForm.FormD);
            char[] filtered = decomposed
                                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                                .ToArray();
            value = new String(filtered);
            return value;
        }

        private string getFakeNameGeneratorCountry(string country)
        {
            country = HttpUtility.UrlDecode(country);

            return (new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("united states", ""),
                new KeyValuePair<string, string>("australia", "au-"),
                new KeyValuePair<string, string>("canada", "ca-"),
                new KeyValuePair<string, string>("united kingdom", "uk-"),
                new KeyValuePair<string, string>("brazil", "br-"),
                new KeyValuePair<string, string>("france", "fr-"),
                new KeyValuePair<string, string>("germany", "de-"),
                new KeyValuePair<string, string>("italy", "it-"),
                new KeyValuePair<string, string>("spain", "es-")

            }).Where(kv => kv.Key == country.ToLower())
            .Select(kv => kv.Value).FirstOrDefault();
        }

        private string getSevisCountry(string country)
        {
            country = HttpUtility.UrlDecode(country);

            return (new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("united states", ""),
                new KeyValuePair<string, string>("aruba", "AA"),
                new KeyValuePair<string, string>("antigua and barbuda", "AC"),
                new KeyValuePair<string, string>("united arab emirates", "AE"),
                new KeyValuePair<string, string>("afghanistan", "AF"),
                new KeyValuePair<string, string>("algeria", "AG"),
                new KeyValuePair<string, string>("azerbaijan", "AJ"),
                new KeyValuePair<string, string>("albania", "AL"),
                new KeyValuePair<string, string>("armenia", "AM"),
                new KeyValuePair<string, string>("andorra", "AN"),
                new KeyValuePair<string, string>("angola", "AO"),
                new KeyValuePair<string, string>("argentina", "AR"),
                new KeyValuePair<string, string>("australia", "AS"),
                new KeyValuePair<string, string>("ashmore and cartier islands", "AT"),
                new KeyValuePair<string, string>("austria", "AU"),
                new KeyValuePair<string, string>("anguilla", "AV"),
                new KeyValuePair<string, string>("akrotiri", "AX"),
                new KeyValuePair<string, string>("bahrain", "BA"),
                new KeyValuePair<string, string>("barbados", "BB"),
                new KeyValuePair<string, string>("botswana", "BC"),
                new KeyValuePair<string, string>("bermuda", "BD"),
                new KeyValuePair<string, string>("belgium", "BE"),
                new KeyValuePair<string, string>("bahamas, the", "BF"),
                new KeyValuePair<string, string>("bangladesh", "BG"),
                new KeyValuePair<string, string>("belize", "BH"),
                new KeyValuePair<string, string>("bosnia and herzegovina", "BK"),
                new KeyValuePair<string, string>("bolivia", "BL"),
                new KeyValuePair<string, string>("burma   ", "BM"),
                new KeyValuePair<string, string>("benin", "BN"),
                new KeyValuePair<string, string>("belarus", "BO"),
                new KeyValuePair<string, string>("solomon islands", "BP"),
                new KeyValuePair<string, string>("brazil", "BR"),
                new KeyValuePair<string, string>("bassas da india", "BS"),
                new KeyValuePair<string, string>("bhutan", "BT"),
                new KeyValuePair<string, string>("bulgaria", "BU"),
                new KeyValuePair<string, string>("bouvet island", "BV"),
                new KeyValuePair<string, string>("brunei", "BX"),
                new KeyValuePair<string, string>("burundi", "BY"),
                new KeyValuePair<string, string>("canada", "CA"),
                new KeyValuePair<string, string>("cambodia", "CB"),
                new KeyValuePair<string, string>("chad", "CD"),
                new KeyValuePair<string, string>("sri lanka", "CE"),
                new KeyValuePair<string, string>("congo (brazzaville)", "CF"),
                new KeyValuePair<string, string>("congo (kinshasa)", "CG"),
                new KeyValuePair<string, string>("china", "CH"),
                new KeyValuePair<string, string>("chile", "CI"),
                new KeyValuePair<string, string>("cayman islands", "CJ"),
                new KeyValuePair<string, string>("cocos (keeling) islands", "CK"),
                new KeyValuePair<string, string>("cameroon", "CM"),
                new KeyValuePair<string, string>("comoros", "CN"),
                new KeyValuePair<string, string>("colombia", "CO"),
                new KeyValuePair<string, string>("coral sea islands", "CR"),
                new KeyValuePair<string, string>("costa rica", "CS"),
                new KeyValuePair<string, string>("central african republic", "CT"),
                new KeyValuePair<string, string>("cuba", "CU"),
                new KeyValuePair<string, string>("cape verde", "CV"),
                new KeyValuePair<string, string>("cook islands", "CW"),
                new KeyValuePair<string, string>("cyprus", "CY"),
                new KeyValuePair<string, string>("denmark", "DA"),
                new KeyValuePair<string, string>("djibouti", "DJ"),
                new KeyValuePair<string, string>("dominica", "DO"),
                new KeyValuePair<string, string>("dominican republic", "DR"),
                new KeyValuePair<string, string>("dhekelia", "DX"),
                new KeyValuePair<string, string>("ecuador", "EC"),
                new KeyValuePair<string, string>("egypt", "EG"),
                new KeyValuePair<string, string>("ireland", "EI"),
                new KeyValuePair<string, string>("equatorial guinea", "EK"),
                new KeyValuePair<string, string>("estonia", "EN"),
                new KeyValuePair<string, string>("eritrea", "ER"),
                new KeyValuePair<string, string>("el salvador", "ES"),
                new KeyValuePair<string, string>("ethiopia", "ET"),
                new KeyValuePair<string, string>("europa island", "EU"),
                new KeyValuePair<string, string>("czech republic", "EZ"),
                new KeyValuePair<string, string>("french guiana", "FG"),
                new KeyValuePair<string, string>("finland", "FI"),
                new KeyValuePair<string, string>("fiji", "FJ"),
                new KeyValuePair<string, string>("falkland islands (islas malvinas)", "FK"),
                new KeyValuePair<string, string>("micronesia, federated states of", "FM"),
                new KeyValuePair<string, string>("faroe islands", "FO"),
                new KeyValuePair<string, string>("french polynesia", "FP"),
                new KeyValuePair<string, string>("france", "FR"),
                new KeyValuePair<string, string>("french southern and antarctic lands", "FS"),
                new KeyValuePair<string, string>("gambia, the", "GA"),
                new KeyValuePair<string, string>("gabon", "GB"),
                new KeyValuePair<string, string>("georgia", "GG"),
                new KeyValuePair<string, string>("ghana", "GH"),
                new KeyValuePair<string, string>("gibraltar", "GI"),
                new KeyValuePair<string, string>("grenada", "GJ"),
                new KeyValuePair<string, string>("guernsey", "GK"),
                new KeyValuePair<string, string>("greenland", "GL"),
                new KeyValuePair<string, string>("germany", "GM"),
                new KeyValuePair<string, string>("glorioso islands", "GO"),
                new KeyValuePair<string, string>("guadeloupe", "GP"),
                new KeyValuePair<string, string>("greece", "GR"),
                new KeyValuePair<string, string>("guatemala", "GT"),
                new KeyValuePair<string, string>("guinea", "GV"),
                new KeyValuePair<string, string>("guyana", "GY"),
                new KeyValuePair<string, string>("gaza strip", "GZ"),
                new KeyValuePair<string, string>("haiti", "HA"),
                new KeyValuePair<string, string>("hong kong", "HK"),
                new KeyValuePair<string, string>("heard island and mcdonald slands", "HM"),
                new KeyValuePair<string, string>("honduras", "HO"),
                new KeyValuePair<string, string>("croatia", "HR"),
                new KeyValuePair<string, string>("hungary", "HU"),
                new KeyValuePair<string, string>("iceland", "IC"),
                new KeyValuePair<string, string>("indonesia", "ID"),
                new KeyValuePair<string, string>("isle of man", "IM"),
                new KeyValuePair<string, string>("india", "IN"),
                new KeyValuePair<string, string>("british indian ocean territory", "IO"),
                new KeyValuePair<string, string>("clipperton island", "IP"),
                new KeyValuePair<string, string>("iran", "IR"),
                new KeyValuePair<string, string>("israel", "IS"),
                new KeyValuePair<string, string>("italy", "IT"),
                new KeyValuePair<string, string>("cote d ivoire", "IV"),
                new KeyValuePair<string, string>("iraq", "IZ"),
                new KeyValuePair<string, string>("japan", "JA"),
                new KeyValuePair<string, string>("jersey", "JE"),
                new KeyValuePair<string, string>("jamaica", "JM"),
                new KeyValuePair<string, string>("jan mayen", "JN"),
                new KeyValuePair<string, string>("jordan", "JO"),
                new KeyValuePair<string, string>("juan de nova island", "JU"),
                new KeyValuePair<string, string>("kenya", "KE"),
                new KeyValuePair<string, string>("kyrgyzstan", "KG"),
                new KeyValuePair<string, string>("north korea", "KN"),
                new KeyValuePair<string, string>("kiribati", "KR"),
                new KeyValuePair<string, string>("south korea", "KS"),
                new KeyValuePair<string, string>("christmas island", "KT"),
                new KeyValuePair<string, string>("kuwait", "KU"),
                new KeyValuePair<string, string>("kosovo", "KV"),
                new KeyValuePair<string, string>("kazakhstan", "KZ"),
                new KeyValuePair<string, string>("laos", "LA"),
                new KeyValuePair<string, string>("lebanon", "LE"),
                new KeyValuePair<string, string>("latvia", "LG"),
                new KeyValuePair<string, string>("lithuania", "LH"),
                new KeyValuePair<string, string>("liberia", "LI"),
                new KeyValuePair<string, string>("slovakia", "LO"),
                new KeyValuePair<string, string>("liechtenstein", "LS"),
                new KeyValuePair<string, string>("lesotho", "LT"),
                new KeyValuePair<string, string>("luxembourg", "LU"),
                new KeyValuePair<string, string>("libya", "LY"),
                new KeyValuePair<string, string>("madagascar", "MA"),
                new KeyValuePair<string, string>("martinique", "MB"),
                new KeyValuePair<string, string>("macau", "MC"),
                new KeyValuePair<string, string>("moldova", "MD"),
                new KeyValuePair<string, string>("mayotte", "MF"),
                new KeyValuePair<string, string>("mongolia", "MG"),
                new KeyValuePair<string, string>("montserrat", "MH"),
                new KeyValuePair<string, string>("malawi", "MI"),
                new KeyValuePair<string, string>("montenegro", "MJ"),
                new KeyValuePair<string, string>("macedonia", "MK"),
                new KeyValuePair<string, string>("mali", "ML"),
                new KeyValuePair<string, string>("monaco", "MN"),
                new KeyValuePair<string, string>("morocco", "MO"),
                new KeyValuePair<string, string>("mauritius", "MP"),
                new KeyValuePair<string, string>("mauritania", "MR"),
                new KeyValuePair<string, string>("malta", "MT"),
                new KeyValuePair<string, string>("oman", "MU"),
                new KeyValuePair<string, string>("maldives", "MV"),
                new KeyValuePair<string, string>("mexico", "MX"),
                new KeyValuePair<string, string>("malaysia", "MY"),
                new KeyValuePair<string, string>("mozambique", "MZ"),
                new KeyValuePair<string, string>("new caledonia", "NC"),
                new KeyValuePair<string, string>("niue", "NE"),
                new KeyValuePair<string, string>("norfolk island", "NF"),
                new KeyValuePair<string, string>("niger", "NG"),
                new KeyValuePair<string, string>("vanuatu", "NH"),
                new KeyValuePair<string, string>("nigeria", "NI"),
                new KeyValuePair<string, string>("netherlands", "NL"),
                new KeyValuePair<string, string>("sint maarten", "NN"),
                new KeyValuePair<string, string>("norway", "NO"),
                new KeyValuePair<string, string>("nepal", "NP"),
                new KeyValuePair<string, string>("nauru", "NR"),
                new KeyValuePair<string, string>("suriname", "NS"),
                new KeyValuePair<string, string>("nicaragua", "NU"),
                new KeyValuePair<string, string>("new zealand", "NZ"),
                new KeyValuePair<string, string>("south sudan", "OD"),
                new KeyValuePair<string, string>("paraguay", "PA"),
                new KeyValuePair<string, string>("pitcairn islands", "PC"),
                new KeyValuePair<string, string>("peru", "PE"),
                new KeyValuePair<string, string>("paracel islands", "PF"),
                new KeyValuePair<string, string>("spratly islands", "PG"),
                new KeyValuePair<string, string>("pakistan", "PK"),
                new KeyValuePair<string, string>("etorofu, habomai, kunashiri, and shikotan islands", "PJ"),
                new KeyValuePair<string, string>("poland", "PL"),
                new KeyValuePair<string, string>("panama", "PM"),
                new KeyValuePair<string, string>("portugal", "PO"),
                new KeyValuePair<string, string>("papua new guinea", "PP"),
                new KeyValuePair<string, string>("palau", "PS"),
                new KeyValuePair<string, string>("guinea-bissau", "PU"),
                new KeyValuePair<string, string>("qatar", "QA"),
                new KeyValuePair<string, string>("reunion", "RE"),
                new KeyValuePair<string, string>("serbia", "RI"),
                new KeyValuePair<string, string>("marshall islands", "RM"),
                new KeyValuePair<string, string>("saint martin", "RN"),
                new KeyValuePair<string, string>("romania", "RO"),
                new KeyValuePair<string, string>("philippines", "RP"),
                new KeyValuePair<string, string>("russia", "RS"),
                new KeyValuePair<string, string>("rwanda", "RW"),
                new KeyValuePair<string, string>("saudi arabia", "SA"),
                new KeyValuePair<string, string>("saint pierre and miquelon", "SB"),
                new KeyValuePair<string, string>("saint kitts and nevis", "SC"),
                new KeyValuePair<string, string>("seychelles", "SE"),
                new KeyValuePair<string, string>("south africa", "SF"),
                new KeyValuePair<string, string>("senegal", "SG"),
                new KeyValuePair<string, string>("saint helena, ascension, and tristan da cunha", "SH"),
                new KeyValuePair<string, string>("slovenia", "SI"),
                new KeyValuePair<string, string>("sierra leone", "SL"),
                new KeyValuePair<string, string>("san marino", "SM"),
                new KeyValuePair<string, string>("singapore", "SN"),
                new KeyValuePair<string, string>("somalia", "SO"),
                new KeyValuePair<string, string>("spain", "SP"),
                new KeyValuePair<string, string>("saint lucia", "ST"),
                new KeyValuePair<string, string>("sudan", "SU"),
                new KeyValuePair<string, string>("svalbard", "SV"),
                new KeyValuePair<string, string>("sweden", "SW"),
                new KeyValuePair<string, string>("south georgia and the south sandwich islands", "SX"),
                new KeyValuePair<string, string>("syria", "SY"),
                new KeyValuePair<string, string>("switzerland", "SZ"),
                new KeyValuePair<string, string>("saint barthelemy", "TB"),
                new KeyValuePair<string, string>("trinidad and tobago", "TD"),
                new KeyValuePair<string, string>("tromelin island", "TE"),
                new KeyValuePair<string, string>("thailand", "TH"),
                new KeyValuePair<string, string>("tajikistan", "TI"),
                new KeyValuePair<string, string>("turks and caicos islands", "TK"),
                new KeyValuePair<string, string>("tokelau", "TL"),
                new KeyValuePair<string, string>("tonga", "TN"),
                new KeyValuePair<string, string>("togo", "TO"),
                new KeyValuePair<string, string>("sao tome and principe", "TP"),
                new KeyValuePair<string, string>("tunisia", "TS"),
                new KeyValuePair<string, string>("timor-leste", "TT"),
                new KeyValuePair<string, string>("turkey", "TU"),
                new KeyValuePair<string, string>("tuvalu", "TV"),
                new KeyValuePair<string, string>("taiwan", "TW"),
                new KeyValuePair<string, string>("turkmenistan", "TX"),
                new KeyValuePair<string, string>("tanzania", "TZ"),
                new KeyValuePair<string, string>("neutral zone", "U2"),
                new KeyValuePair<string, string>("stateless", "U3"),
                new KeyValuePair<string, string>("unknown", "U5"),
                new KeyValuePair<string, string>("curacao", "UC"),
                new KeyValuePair<string, string>("uganda", "UG"),
                new KeyValuePair<string, string>("united kingdom", "UK"),
                new KeyValuePair<string, string>("ukraine", "UP"),
                new KeyValuePair<string, string>("burkina faso", "UV"),
                new KeyValuePair<string, string>("uruguay", "UY"),
                new KeyValuePair<string, string>("uzbekistan", "UZ"),
                new KeyValuePair<string, string>("saint vincent and the grenadines", "VC"),
                new KeyValuePair<string, string>("venezuela", "VE"),
                new KeyValuePair<string, string>("british virgin islands", "VI"),
                new KeyValuePair<string, string>("vietnam", "VM"),
                new KeyValuePair<string, string>("vatican city", "VT"),
                new KeyValuePair<string, string>("namibia", "WA"),
                new KeyValuePair<string, string>("west bank", "WE"),
                new KeyValuePair<string, string>("wallis and futuna", "WF"),
                new KeyValuePair<string, string>("western sahara", "WI"),
                new KeyValuePair<string, string>("samoa", "WS"),
                new KeyValuePair<string, string>("swaziland", "WZ"),
                new KeyValuePair<string, string>("yemen", "YM"),
                new KeyValuePair<string, string>("zambia", "ZA"),
                new KeyValuePair<string, string>("zimbabwe", "ZI")
            }).Where(kv => kv.Key == country.ToLower())
            .Select(kv => kv.Value).FirstOrDefault();
        }

        public Company Company()
        {
            var htmlString = _generatorService.GenerateCompanyName();
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(htmlString);

            var nodes = htmlDoc.DocumentNode.Descendants(0).Where(n => n.HasClass("content-list"));

            var list = nodes.ElementAt(0).Descendants("li").Select(n => new Company
            {
                Name = $"{n.ChildNodes[1].InnerText} Test Data",
                Url = $"www.{n.ChildNodes[1].InnerText.Replace(" ", "")}.test"
            }).ToList();

            var einList = new List<string>();
            list.ForEach((company) =>
            {
                string ein = "";

                while (string.IsNullOrEmpty(ein) || einList.Contains(ein))
                {
                    ein = _generatorService.GenerateEIN();
                }

                company.EIN = ein;

                einList.Add(ein);
            });

            return list.FirstOrDefault();
        }
    }
}
