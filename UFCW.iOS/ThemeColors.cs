using System.Collections.Generic;
using Xamarin.Forms;
using UXDivers.Artina.Shared;

namespace UFCW.iOS
 {
	internal class ThemeColors : ThemeColorsBase
	{
		private readonly static Dictionary<string, Color> _themeColors = new Dictionary<string, Color>
		{
			{ "AccentColor", Color.FromHex("#02447a") },
			{ "BaseTextColor", Color.FromHex("#666666") },
			{ "BackgroundColor", Color.FromHex("#57a9ed") },
			{ "NoDataTextColor", Color.FromHex("#666666") },
			{ "InverseTextColor", Color.White },
			{ "BrandColor", Color.FromHex("#ad1457") },
			{ "BrandNameColor", Color.FromHex("#FFFFFF") },
			{ "BaseLightTextColor", Color.FromHex("#7b7b7b") },
			{ "OverImageTextColor", Color.FromHex("#FFFFFF") },
			{ "EcommercePromoTextColor", Color.FromHex("#FFFFFF") },
			{ "SocialHeaderTextColor", Color.FromHex("#666666") },
			{ "ArticleHeaderBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "CustomNavBarTextColor", Color.FromHex("#FFFFFF") },
			{ "ListViewItemTextColor", Color.FromHex("#666666") },
			{ "AboutHeaderBackgroundColor", Color.FromHex("#FFFFFF") },
			{ "DetailLabelHeaderColor", Color.FromHex("#000000") },
			{ "BasePageColor", Color.FromHex("#FFFFFF") },
			{ "BaseTabbedPageColor", Color.FromHex("#fafafa") },
			{ "MainWrapperBackgroundColor", Color.FromHex("#EFEFEF") },
			{ "CategoriesListIconColor", Color.FromHex("#55000000") },
			{ "DashboardIconColor", Color.FromHex("#003663") },
			{ "ComplementColor", Color.FromHex("#525ABB") },
			{ "TranslucidBlack", Color.FromHex("#44000000") },
			{ "TranslucidWhite", Color.FromHex("#22ffffff") },
			{ "OkColor", Color.FromHex("#22c064") },
			{ "ArtinaLoginEntryStyle", Color.Red },
			{ "WarningColor", Color.FromHex("#ffc107") },
			{ "ErrorColor", Color.Red },
			{ "NotificationColor", Color.FromHex("#1274d1") },
			{ "SaveButtonColor", Color.FromHex("#22c064") },
			{ "DeleteButtonColor", Color.FromHex("#D50000") },
			{ "LabelButtonColor", Color.FromHex("#ffffff") },
			{ "ButtonIconLabelColor", Color.FromHex("#7d7d7d") },
			{ "PlaceholderColor", Color.FromHex("#22ffffff") },
			{ "PlaceholderColorEntry", Color.FromHex("#FFFFFF") },
			{ "RoundedLabelBackgroundColor", Color.FromHex("#525ABB") },
			{ "MainMenuHeaderBackgroundColor", Color.FromHex("#384F63") },
			{ "MainMenuBackgroundColor", Color.FromHex("#F1F3F5") },
			{ "MainMenuSeparatorColor", Color.Transparent },
			{ "MainMenuTextColor", Color.FromHex("#666666") },
			{ "MainMenuIconColor", Color.FromHex("#666666") },
			{ "ListViewSeparatorColor", Color.FromHex("#D3D3D3") },
			{ "BaseSeparatorColor", Color.FromHex("#7b7b7b") },
			{ "ChatRightBalloonBackgroundColor", Color.FromHex("#525ABB") },
			{ "ChatBalloonFooterTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatRightTextColor", Color.FromHex("#FFFFFF") },
			{ "ChatLeftTextColor", Color.FromHex("#FFFFFF") }
		};
		public ThemeColors() : base(_themeColors) {}
	}
}
