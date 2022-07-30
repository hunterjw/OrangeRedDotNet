﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace OrangeRedDotNet.Models.Account
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageCode
    {
        [Description("Afrikaans (af)")] af,
        [Description("አማርኛ (am)")] am,
        [Description("العربية (ar)")] ar,
        [Description("Mapudungun (arn)")] arn,
        [Description("অসমীয়া (as)")] @as,
        [Description("azərbaycan (az)")] az,
        [Description("Башҡорт (ba)")] ba,
        [Description("Беларуская (be)")] be,
        [Description("български (bg)")] bg,
        [Description("বাংলা (bn)")] bn,
        [Description("བོད་ཡིག (bo)")] bo,
        [Description("brezhoneg (br)")] br,
        [Description("bosanski (bs)")] bs,
        [Description("català (ca)")] ca,
        [Description("Corsu (co)")] co,
        [Description("čeština (cs)")] cs,
        [Description("Cymraeg (cy)")] cy,
        [Description("dansk (da)")] da,
        [Description("Deutsch (de)")] de,
        [Description("dolnoserbšćina (dsb)")] dsb,
        [Description("ދިވެހިބަސް (dv)")] dv,
        [Description("Ελληνικά (el)")] el,
        [Description("English (en)")] en,
        [Description("español (es)")] es,
        [Description("eesti (et)")] et,
        [Description("euskara (eu)")] eu,
        [Description("فارسی (fa)")] fa,
        [Description("suomi (fi)")] fi,
        [Description("Filipino (fil)")] fil,
        [Description("føroyskt (fo)")] fo,
        [Description("français (fr)")] fr,
        [Description("Frysk (fy)")] fy,
        [Description("Gaeilge (ga)")] ga,
        [Description("Gàidhlig (gd)")] gd,
        [Description("galego (gl)")] gl,
        [Description("Schwiizertüütsch (gsw)")] gsw,
        [Description("ગુજરાતી (gu)")] gu,
        [Description("Hausa (ha)")] ha,
        [Description("עברית (he)")] he,
        [Description("हिन्दी (hi)")] hi,
        [Description("hrvatski (hr)")] hr,
        [Description("hornjoserbšćina (hsb)")] hsb,
        [Description("magyar (hu)")] hu,
        [Description("Հայերեն (hy)")] hy,
        [Description("Indonesia (id)")] id,
        [Description("Igbo (ig)")] ig,
        [Description("ꆈꌠꁱꂷ (ii)")] ii,
        [Description("íslenska (is)")] @is,
        [Description("italiano (it)")] it,
        [Description("Inuktitut (iu)")] iu,
        [Description("日本語 (ja)")] ja,
        [Description("ქართული (ka)")] ka,
        [Description("қазақ тілі (kk)")] kk,
        [Description("kalaallisut (kl)")] kl,
        [Description("ខ្មែរ (km)")] km,
        [Description("ಕನ್ನಡ (kn)")] kn,
        [Description("한국어 (ko)")] ko,
        [Description("कोंकणी (kok)")] kok,
        [Description("кыргызча (ky)")] ky,
        [Description("Lëtzebuergesch (lb)")] lb,
        [Description("ລາວ (lo)")] lo,
        [Description("lietuvių (lt)")] lt,
        [Description("latviešu (lv)")] lv,
        [Description("te reo Māori (mi)")] mi,
        [Description("македонски (mk)")] mk,
        [Description("മലയാളം (ml)")] ml,
        [Description("монгол (mn)")] mn,
        [Description("Kanien’kéha (moh)")] moh,
        [Description("मराठी (mr)")] mr,
        [Description("Melayu (ms)")] ms,
        [Description("Malti (mt)")] mt,
        [Description("ဗမာ (my)")] my,
        [Description("norsk bokmål (nb)")] nb,
        [Description("नेपाली (ne)")] ne,
        [Description("Nederlands (nl)")] nl,
        [Description("norsk nynorsk (nn)")] nn,
        [Description("norsk (no)")] no,
        [Description("Sesotho sa Leboa (nso)")] nso,
        [Description("Occitan (oc)")] oc,
        [Description("ଓଡ଼ିଆ (or)")] or,
        [Description("ਪੰਜਾਬੀ (pa)")] pa,
        [Description("polski (pl)")] pl,
        [Description("درى (prs)")] prs,
        [Description("پښتو (ps)")] ps,
        [Description("português (pt)")] pt,
        [Description("K'iche' (qut)")] qut,
        [Description("Runasimi (quz)")] quz,
        [Description("rumantsch (rm)")] rm,
        [Description("română (ro)")] ro,
        [Description("русский (ru)")] ru,
        [Description("Kinyarwanda (rw)")] rw,
        [Description("संस्कृत (sa)")] sa,
        [Description("саха тыла (sah)")] sah,
        [Description("davvisámegiella (se)")] se,
        [Description("සිංහල (si)")] si,
        [Description("slovenčina (sk)")] sk,
        [Description("slovenščina (sl)")] sl,
        [Description("åarjelsaemiengïele (sma)")] sma,
        [Description("julevusámegiella (smj)")] smj,
        [Description("anarâškielâ (smn)")] smn,
        [Description("sää´mǩiõll (sms)")] sms,
        [Description("shqip (sq)")] sq,
        [Description("srpski (sr)")] sr,
        [Description("svenska (sv)")] sv,
        [Description("Kiswahili (sw)")] sw,
        [Description("ܣܘܪܝܝܐ (syr)")] syr,
        [Description("தமிழ் (ta)")] ta,
        [Description("తెలుగు (te)")] te,
        [Description("Тоҷикӣ (tg)")] tg,
        [Description("ไทย (th)")] th,
        [Description("Türkmen dili (tk)")] tk,
        [Description("Setswana (tn)")] tn,
        [Description("Türkçe (tr)")] tr,
        [Description("Татар (tt)")] tt,
        [Description("Tamaziɣt n laṭlaṣ (tzm)")] tzm,
        [Description("ئۇيغۇرچە (ug)")] ug,
        [Description("українська (uk)")] uk,
        [Description("اُردو (ur)")] ur,
        [Description("o‘zbek (uz)")] uz,
        [Description("Tiếng Việt (vi)")] vi,
        [Description("Wolof (wo)")] wo,
        [Description("isiXhosa (xh)")] xh,
        [Description("Èdè Yorùbá (yo)")] yo,
        [Description("中文 (zh)")] zh,
        [Description("isiZulu (zu)")] zu,
    }
}