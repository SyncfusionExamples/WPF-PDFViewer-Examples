using System.Reflection;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Syncfusion.Themes.FluentLight.WPF
{
    /// <exclude/>
    public class FluentLightSkinHelper : SkinHelper
    {
        [Obsolete("GetDictonaries is deprecated, please use GetDictionaries instead.")]
        public override List<string> GetDictonaries(string type, string style)
        {
            return GetDictionaries(type, style);
        }

        #region Fluent Settings
        public override void SetFluentSettings(DependencyObject obj, HoverEffect hoverEffectMode, PressedEffect pressedEffectMode)
        {
            base.SetFluentSettings(obj, hoverEffectMode, pressedEffectMode);
            FluentHelper.SetHoverEffectMode(obj, hoverEffectMode);
            FluentHelper.SetPressedEffectMode(obj, pressedEffectMode);
        }
        #endregion

        public override List<string> GetDictionaries(String type, string style)
        {
            string rootStylePath = "/Syncfusion.Themes.FluentLight.WPF;component/";
            List<string> styles = new List<string>();
            # region Switch

			switch (type)
			{
				case "MSControls":
					styles.Add(rootStylePath + "MSControl/ToolTip.xaml");
					styles.Add(rootStylePath + "MSControl/Menu.xaml");
					styles.Add(rootStylePath + "MSControl/Separator.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphButton.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphRepeatButton.xaml");
					styles.Add(rootStylePath + "MSControl/Slider.xaml");
					styles.Add(rootStylePath + "MSControl/RepeatButton.xaml");
					styles.Add(rootStylePath + "MSControl/TextBox.xaml");
					styles.Add(rootStylePath + "MSControl/ProgressBar.xaml");
					styles.Add(rootStylePath + "MSControl/Window.xaml");
					styles.Add(rootStylePath + "MSControl/TreeView.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphTreeExpander.xaml");
					styles.Add(rootStylePath + "MSControl/Hyperlink.xaml");
					styles.Add(rootStylePath + "MSControl/ComboBox.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphDropdownExpander.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphEditableDropdownExpander.xaml");
					styles.Add(rootStylePath + "MSControl/ScrollViewer.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphToggleButton.xaml");
					break;
				case "ColorPickerPalette":
					styles.Add(rootStylePath + "ColorPickerPalette/ColorPickerPalette.xaml");
					break;
				case "UpDown":
					styles.Add(rootStylePath + "UpDown/UpDown.xaml");
					break;
				case "DoubleTextBox":
					styles.Add(rootStylePath + "DoubleTextBox/DoubleTextBox.xaml");
					break;
				case "PdfViewerControl":
					styles.Add(rootStylePath + "PdfViewerControl/PdfViewerControl.xaml");
					break;
				case "Common":
					styles.Add(rootStylePath + "Common/Common.xaml");
					break;
				case "Brushes":
					styles.Add(rootStylePath + "Common/Brushes.xaml");
					break;
			}

            # endregion

            return styles;
        }
    }

    #region Palette enum

	/// <summary>
	/// Specifies the different set of palette color combination to apply on specific theme.
	/// </summary>
	public enum FluentPalette
	{
		/// <summary>
		/// The Default palette primary colors will be applied for specific theme.
		/// </summary>
		Default,
		/// <summary>
		/// The PinkRed palette primary colors will be applied for specific theme.
		/// </summary>
		PinkRed,
		/// <summary>
		/// The Red palette primary colors will be applied for specific theme.
		/// </summary>
		Red,
		/// <summary>
		/// The RedOrange palette primary colors will be applied for specific theme.
		/// </summary>
		RedOrange,
		/// <summary>
		/// The Orange palette primary colors will be applied for specific theme.
		/// </summary>
		Orange,
		/// <summary>
		/// The Green palette primary colors will be applied for specific theme.
		/// </summary>
		Green,
		/// <summary>
		/// The GreenCyan palette primary colors will be applied for specific theme.
		/// </summary>
		GreenCyan,
		/// <summary>
		/// The Cyan palette primary colors will be applied for specific theme.
		/// </summary>
		Cyan,
		/// <summary>
		/// The CyanBlue palette primary colors will be applied for specific theme.
		/// </summary>
		CyanBlue,
		/// <summary>
		/// The Blue palette primary colors will be applied for specific theme.
		/// </summary>
		Blue,
		/// <summary>
		/// The BlueMegenta palette primary colors will be applied for specific theme.
		/// </summary>
		BlueMegenta,
		/// <summary>
		/// The Megenta palette primary colors will be applied for specific theme.
		/// </summary>
		Megenta,
		/// <summary>
		/// The MegentaPink palette primary colors will be applied for specific theme.
		/// </summary>
		MegentaPink
	}
    #endregion

    /// <summary>
    /// Represents a class that holds the respective theme color and common key values for customization
    /// </summary>
    public class FluentLightThemeSettings: IThemeSetting
    {
        /// <summary>
        /// Constructor to create an instance of FluentLightThemeSettings.
        /// </summary>
        public FluentLightThemeSettings()
        {
            #region Initialize Value 
			HeaderFontSize = 16;
			SubHeaderFontSize = 14;
			TitleFontSize = 14;
			SubTitleFontSize = 12;
			BodyFontSize = 12;
			BodyAltFontSize = 10;

            #endregion
        }

        #region Palette Properties
        /// <summary>
        /// Gets or sets the palette primary colors to be set for specific theme. 
        /// </summary>
        /// <value>
        /// <para>One of the <see cref="Palette"/> enumeration that specifies the palette to be chosen.</para>
        /// <para>The default value is <see cref="FluentPalette.Default"/>.</para>
        /// <para><b>Fields:</b></para>
        /// <list type="table">
        /// <listheader>
        /// <term>Enumeration</term>
        /// <description>Description.</description>
        /// </listheader>
		/// <item>
		/// <term><see cref="FluentPalette.Default"/></term>
		/// <description>The Default palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.PinkRed"/></term>
		/// <description>The PinkRed palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Red"/></term>
		/// <description>The Red palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.RedOrange"/></term>
		/// <description>The RedOrange palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Orange"/></term>
		/// <description>The Orange palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Green"/></term>
		/// <description>The Green palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.GreenCyan"/></term>
		/// <description>The GreenCyan palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Cyan"/></term>
		/// <description>The Cyan palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.CyanBlue"/></term>
		/// <description>The CyanBlue palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Blue"/></term>
		/// <description>The Blue palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.BlueMegenta"/></term>
		/// <description>The BlueMegenta palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.Megenta"/></term>
		/// <description>The Megenta palette primary colors will be applied for specific theme.</description>
		/// </item>
		/// <item>
		/// <term><see cref="FluentPalette.MegentaPink"/></term>
		/// <description>The MegentaPink palette primary colors will be applied for specific theme.</description>
		/// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code language="C#">
        /// <![CDATA[
        /// FluentLightThemeSettings themeSettings = new FluentLightThemeSettings();
		/// themeSettings.Palette = FluentPalette.PinkRed;
        /// SfSkinManager.RegisterThemeSettings("FluentLight", themeSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <remarks>
        /// Applicable only for <see href="https://help.syncfusion.com/wpf/themes/skin-manager#themes-list">ThemeStudio specific themes.</see>
        /// </remarks>
        public FluentPalette Palette { get; set; }
        #endregion

        #region Properties


		/// <summary>
		/// Gets or sets the font size of header related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.HeaderFontSize = 16;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double HeaderFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of sub header related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.SubHeaderFontSize = 14;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double SubHeaderFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of title related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.TitleFontSize = 14;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double TitleFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of sub title related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.SubTitleFontSize = 12;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double SubTitleFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.BodyFontSize = 12;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double BodyFontSize { get; set; }


		/// <summary>
		/// Gets or sets the alternate font size of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.Body AltFontSize = 10;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Double BodyAltFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font family of text in control for selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.FontFamily = new FontFamily("Callibri");
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public FontFamily FontFamily { get; set; }

		private Brush primarybackground;


		/// <summary>
		/// Gets or sets the primary background color of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.PrimaryBackground = Brushes.Red;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Brush PrimaryBackground
		{
			get
			{
				return primarybackground;
			}
			set
			{
				primarybackground = value;
				PrimaryColorForeground = value;
				PrimaryColorDark3 = ThemeSettingsHelper.GetDerivationColor(value, 0.2, 0);
				PrimaryColorDark2 = ThemeSettingsHelper.GetDerivationColor(value, 0.15, 0);
				PrimaryColorDark1 = ThemeSettingsHelper.GetDerivationColor(value, 0.05, 0);
				PrimaryColorLight1 = ThemeSettingsHelper.GetDerivationColor(value, -0.1, 0);
				PrimaryColorLight2 = ThemeSettingsHelper.GetDerivationColor(value, -0.15, 0);
				PrimaryColorLight3 = ThemeSettingsHelper.GetDerivationColor(value, -0.25, 0);
				LinkForeground = value;
				PrimaryBackgroundOpacity1 = ThemeSettingsHelper.GetDerivationColor(value, 0, 0.4);
			}
		}


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorForeground { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorDark3 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorDark2 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorDark1 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorLight1 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorLight2 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryColorLight3 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush LinkForeground { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryBackgroundOpacity1 { get; set; }

		private Brush primaryforeground;


		/// <summary>
		/// Gets or sets the primary foreground color of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
		/// fluentLightThemeSettings.PrimaryForeground = Brushes.AntiqueWhite;
		/// SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);
		/// ]]>
		/// </code>
		/// </example>
		public Brush PrimaryForeground
		{
			get
			{
				return primaryforeground;
			}
			set
			{
				primaryforeground = value;
				PrimaryForegroundDisabled = ThemeSettingsHelper.GetDerivationColor(value, 0, 0.5);
			}
		}


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.Browsable(false)]
		public Brush PrimaryForegroundDisabled { get; set; }

        #endregion

        /// <summary>
        /// Helper method to decide on display property name using property mappings 
        /// </summary>
        /// <returns>Dictionary of property mappings</returns>
        /// <exclude/>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public Dictionary<string, string> GetPropertyMappings()
        {
            Dictionary<string, string> propertyMappings = new Dictionary<string, string>();
            #region PropertyMappings
			propertyMappings.Add("HeaderFontSize", "HeaderTextStyle");
			propertyMappings.Add("SubHeaderFontSize", "SubHeaderTextStyle");
			propertyMappings.Add("TitleFontSize", "TitleTextStyle");
			propertyMappings.Add("SubTitleFontSize", "SubTitleTextStyle");
			propertyMappings.Add("BodyFontSize", "BodyTextStyle");
			propertyMappings.Add("BodyAltFontSize", "CaptionText");
			propertyMappings.Add("FontFamily", "ThemeFontFamily");

            #endregion
            return propertyMappings;
        }
    }
}
