//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// DevExpressHelpers.cs
//      This class implements helper routines to handle repetative DevExpress code.
//========================================================================================================================

using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.Data;
using DevExpress.Web;
using DevExpress.Web.Mvc;

namespace SSCasino
{
    public static class DevExpressHelpers
    {
        #region General
        //================================================================================================================
        //================================================================================================================

        public static void Render_CallbackPanel(CallbackPanelSettings controlSettings, string controlName, string controllerName, string actionMethod, string widthClass, bool useBeginCallback, bool useEndCallback)
        //================================================================================================================
        // This routine generates code to build a DevExpress callback panel
        //
        // Parameters
        //      controlSettings:  Reference to a callback panel control settings object
        //      controlName:      Unique name for this control
        //      controllerName:   Name of the controller that handles the callbacks
        //      actionMethod:     Name of the action method to invoke on callbacks
        //      useBeginCallback: Flag indicating if a BeginCallback method is provided
        //      useEndCallback:   Flag indicating if an EndCallback method is provided
        //================================================================================================================
        {
            // General Settings
            controlSettings.Name = controlName;

            // Server side callbacks
            controlSettings.CallbackRouteValues = new { Controller = controllerName, Action = actionMethod };

            // Set the width using a width class, if supplied
            if (widthClass != "")
                controlSettings.ControlStyle.CssClass = widthClass;
            else
                controlSettings.Width = Unit.Percentage(100);

            // Developer Notes
            //      The names of the client side events are derived from the controlName and the event name
            //
            // Client side events
            if (useBeginCallback)
                controlSettings.ClientSideEvents.BeginCallback = controlName + "_BeginCallback";

            if (useEndCallback)
                controlSettings.ClientSideEvents.EndCallback = controlName + "_EndCallback";
        }

        public static void Render_Button(ButtonSettings buttonSettings, string controlName, string caption, string imagePath, bool useSubmit, string clickHandler, int width = 0)
        //================================================================================================================
        // This routine generates code to build a DevExpress button.
        //
        // Parameters
        //      buttonSettings: Reference to a button control settings object
        //      controlName:    Unique name for this control
        //      caption:        Button text
        //      controlWidth:   Width of the button
        //      imagePath:      Absolute path to the image
        //      useSubmit:      Flag indicating if the button should use submit behavior
        //      clickHandler:   Name of the click handler routine if the buuton is not using submit
        //================================================================================================================
        {
            // General Settings
            buttonSettings.Name = controlName;
            buttonSettings.Text = caption;
            if (width != 0)
                buttonSettings.Width = Unit.Pixel(80);

            // Does this button have an image?
            if (imagePath != "")
            {
                // Assign the image
                buttonSettings.Images.Image.Url = imagePath;
                buttonSettings.ImagePosition = ImagePosition.Left;

                // Apply the image button style
                buttonSettings.ControlStyle.CssClass = "ssc_ImageButton";
            }
            else
            {
                // Apply the standard button style
                buttonSettings.ControlStyle.CssClass = "ssc_FormButton";
            }

            // Submit or click event
            buttonSettings.UseSubmitBehavior = useSubmit;
            if (!useSubmit)
                buttonSettings.ClientSideEvents.Click = clickHandler;
        }

        public static void Render_Textbox(TextBoxSettings textSettings, bool password, bool showErrors, string controlName = "", HorizontalAlign alignment = HorizontalAlign.Left, string styleClass = "", string editMask = "", string lostFocusHandler = "", int width = 0)
        //================================================================================================================
        // This routine generates code to build a DevExpress button.
        //
        // Parameters
        //      textSettings:     Reference to a textbox control settings object
        //      password:         Hide types characters (True, False)
        //      showErrors:       Show model errorts (True, False)
        //      controlName:      (optional) Unique name for this control
        //      alignment:        (optional) Horizontal alignment
        //      styleClass:       (optional) Name of one or more style classes to apply
        //      editMask          (optional) Forces formatted input
        //      lostFocusHandler: (optional) Name of the routine to call when the textbox loses focus
        //      width:            (optional) Width in pixels to assign. If zero 100% width is used.
        //================================================================================================================
        {
            // Control name
            if (controlName != "")
                textSettings.Name = controlName;

            // Width
            if (width != 0)
                textSettings.Width = Unit.Pixel(width);
            else
                textSettings.Width = Unit.Percentage(100);

            // Style class
            textSettings.ControlStyle.HorizontalAlign = alignment;
            if (styleClass != "")
                textSettings.ControlStyle.CssClass = styleClass;

            // Edit mask
            if (editMask != "")
                textSettings.Properties.MaskSettings.Mask = editMask;

            // Lost focus handler
            if (lostFocusHandler != "")
                textSettings.Properties.ClientSideEvents.LostFocus = lostFocusHandler;

            // Password protect
            // Error handling
            textSettings.Properties.Password = password;
            textSettings.ShowModelErrors = showErrors;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // General

        #region PopupForms
        //================================================================================================================
        //================================================================================================================

        public static void Popup_AsSystemDialog(PopupControlSettings controlSettings, string controlName, string controllerName, string actionMethod, bool useBeginCallback, bool useEndCallback, string title, Unit controlWidth, int controlTop)
        //================================================================================================================
        // This routine generates code to build a system dialog using the DevExpress popup control
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //      controlWidth:    Control width
        //      controlTop:      Control top
        //================================================================================================================
        {
            // General Settings
            controlSettings.Name = controlName;
            controlSettings.Top = controlTop;
            controlSettings.Width = controlWidth;

            // Server side callbacks
            controlSettings.CallbackRouteValues = new { Controller = controllerName, Action = actionMethod, Area = "" };

            // Developer Notes
            //      The names of the client side events are derived from the controlName and the event name
            //
            // Begin & End Callback client side events
            if (useBeginCallback)
                controlSettings.ClientSideEvents.BeginCallback = controlName + "_BeginCallback";

            if (useEndCallback)
                controlSettings.ClientSideEvents.EndCallback = controlName + "_EndCallback";

            // Visual settings
            controlSettings.HeaderText = title;
            controlSettings.Styles.Header.Font.Bold = true;
            controlSettings.ShowCloseButton = false;
            controlSettings.ControlStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            controlSettings.PopupAnimationType = AnimationType.Fade;
            controlSettings.CloseAnimationType = AnimationType.Fade;
            controlSettings.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;

            // Style settings
            int backColor = int.Parse(SiteHelpers.DialogBkColor, NumberStyles.HexNumber);
            controlSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            controlSettings.Styles.Content.Paddings.Padding = 0;

            // Functionality settings
            controlSettings.LoadContentViaCallback = LoadContentViaCallback.OnFirstShow;
            controlSettings.ShowOnPageLoad = false;
            controlSettings.AllowDragging = true;
            controlSettings.Modal = true;
        }

        public static void Popup_AsFloatingMenu(PopupControlSettings controlSettings)
        //================================================================================================================
        // This routine generates code to build a popup menu using the DevExpress popup control
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //================================================================================================================
        {
            // Visual settings
            controlSettings.ShowHeader = false;
            controlSettings.ShowFooter = false;
            controlSettings.ControlStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            controlSettings.PopupAnimationType = AnimationType.None;
            controlSettings.ShowCloseButton = false;
            controlSettings.CloseOnEscape = true;
            controlSettings.CloseAnimationType = AnimationType.None;

            // Style settings
            int backColor = int.Parse(SiteHelpers.MenuBackolor, NumberStyles.HexNumber);
            controlSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            controlSettings.Styles.Content.Paddings.Padding = 0;

            // Functionality settings
            controlSettings.ShowOnPageLoad = false;
            controlSettings.AllowDragging = false;
            controlSettings.Modal = false;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // PopupForms

        #region GridViews
        //================================================================================================================
        //================================================================================================================

        public static void GridView_AddColumn(GridViewSettings gridSettings, MVCxGridViewColumnType colType, string fieldName, string colCaption, HorizontalAlign colAlignment, Unit colWidth, string colFormat, ColumnSortOrder colSortOrder)
        //================================================================================================================
        // This routine generates code to add a column to a DevExpress grid
        //
        // Parameters
        //      gridSettings: Reference to a grid view settings object
        //      colType:      Data type of the column
        //      fieldName:    Name of the model property this field is linked to
        //      colCaption:   Column title
        //      colAlignment: Horizontal Alignment
        //      colWidth:     Column width
        //      colFormat:    Column format string
        //      colSortOrder: Column sort order
        //================================================================================================================
        {
            gridSettings.Columns.Add(column =>
            {
                column.FieldName = fieldName;
                column.Caption = colCaption;
                column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                column.ColumnType = colType;
                column.PropertiesEdit.DisplayFormatString = colFormat;
                column.CellStyle.HorizontalAlign = colAlignment;
                column.Width = colWidth;
                column.SortIndex = 1;
                column.SortOrder = colSortOrder;
            });
        }

        public static void GridView_StaticData(GridViewSettings gridSettings, string controlName, Unit controlWidth, int scrollHeight)
        //================================================================================================================
        // This routine generates code to build a DevExpress grid suited for static data...
        //      Read only, No callbacks, displays all data
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //      controlWidth:    Control width
        //      scrollHeight:    Scrollable area
        //================================================================================================================
        {
            // Visual settings
            gridSettings.Name = controlName;
            gridSettings.Width = controlWidth;

            if (scrollHeight != 0)
            {
                gridSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                gridSettings.Settings.VerticalScrollableHeight = scrollHeight;
            }

            // Style settings
            int backColor = int.Parse(SiteHelpers.GridDeadAreaColor, NumberStyles.HexNumber);
            gridSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            gridSettings.ControlStyle.CssClass = "ssc_NoFocusRect";

            // Functionality settings
            gridSettings.KeyboardSupport = true;
            gridSettings.SettingsBehavior.AllowGroup = false;
            gridSettings.SettingsBehavior.AllowSort = false;
            gridSettings.SettingsBehavior.AllowDragDrop = false;
            gridSettings.SettingsBehavior.EnableRowHotTrack = true;
            gridSettings.SettingsBehavior.AllowFocusedRow = true;
            gridSettings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
            gridSettings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }

        public static void GridView_SmallData(GridViewSettings gridSettings, string controlName, Unit controlWidth, string keyField = "", int scrollHeight = 0)
        //================================================================================================================
        // This routine generates code to build a DevExpress grid suited for small amounts of data (5000 rows or less)...
        //      Read only, Callbacks for refresh, displays all data
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //      controlName:     Control name
        //      controlWidth:    Control width
        //      keyField:        (optional) Key data field
        //      scrollHeight:    (optional) Scrollable area
        //================================================================================================================
        {
            // Geneal Settings
            gridSettings.Name = controlName;
            gridSettings.KeyFieldName = keyField;

            // Visual settings
            gridSettings.Width = controlWidth;
            gridSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
            gridSettings.Settings.VerticalScrollableHeight = scrollHeight;

            // Style settings
            int backColor = int.Parse(SiteHelpers.GridDeadAreaColor, NumberStyles.HexNumber);
            gridSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            gridSettings.ControlStyle.CssClass = "ssc_NoFocusRect";

            // Functionality settings
            gridSettings.KeyboardSupport = true;
            gridSettings.SettingsBehavior.AllowGroup = false;
            gridSettings.SettingsBehavior.AllowSort = true;
            gridSettings.SettingsBehavior.AllowDragDrop = false;
            gridSettings.SettingsBehavior.EnableRowHotTrack = true;
            gridSettings.SettingsBehavior.AllowFocusedRow = true;
            gridSettings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
            gridSettings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }

        public static void GridView_PagedData(GridViewSettings gridSettings, string controlName, Unit controlWidth, int pageSize = 100, int scrollHeight = 0, string keyField = "")
        //================================================================================================================
        // This routine generates code to build a DevExpress grid suited for displaying large amounts of data using the
        // standard paging system
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //      controlName:     Control name
        //      controlWidth:    Control width
        //      pageSize:        Number of rows per page
        //      keyField:        (optional) Key data field
        //      scrollHeight:    (optional) Scrollable area
        //================================================================================================================
        {
            // Geneal Settings
            gridSettings.Name = controlName;
            gridSettings.KeyFieldName = keyField;

            // Visual settings
            gridSettings.Width = controlWidth;

            if (scrollHeight > 0)
            {
                gridSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                gridSettings.Settings.VerticalScrollableHeight = scrollHeight;
            }

            // Style settings
            int backColor = int.Parse(SiteHelpers.GridDeadAreaColor, NumberStyles.HexNumber);
            gridSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            gridSettings.ControlStyle.CssClass = "ssc_NoFocusRect";

            // Functionality settings
            gridSettings.KeyboardSupport = true;
            gridSettings.SettingsBehavior.AllowGroup = false;
            gridSettings.SettingsBehavior.AllowSort = true;
            gridSettings.SettingsBehavior.AllowDragDrop = false;
            gridSettings.SettingsBehavior.EnableRowHotTrack = true;
            gridSettings.SettingsBehavior.AllowFocusedRow = true;
            gridSettings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
            gridSettings.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gridSettings.SettingsPager.PageSize = pageSize;
        }

        public static void GridView_VirtualData(GridViewSettings gridSettings, string controlName, Unit controlWidth, int scrollHeight = 0, string keyField = "")
        //================================================================================================================
        // This routine generates code to build a DevExpress grid suited for displaying large amounts of data using the
        // virtual paging system
        //
        // Parameters
        //      controlSettings: Reference to a popup control settings object
        //      controlName:     Control name
        //      controlWidth:    Control width
        //      keyField:        (optional) Key data field
        //      scrollHeight:    (optional) Scrollable area
        //================================================================================================================
        {
            // Geneal Settings
            gridSettings.Name = controlName;
            gridSettings.KeyFieldName = keyField;

            // Visual settings
            gridSettings.Width = controlWidth;

            if (scrollHeight > 0)
            {
                gridSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                gridSettings.Settings.VerticalScrollableHeight = scrollHeight;
            }

            // Style settings
            int backColor = int.Parse(SiteHelpers.GridDeadAreaColor, NumberStyles.HexNumber);
            gridSettings.ControlStyle.BackColor = Color.FromArgb(backColor);
            gridSettings.ControlStyle.CssClass = "ssc_NoFocusRect";

            // Functionality settings
            gridSettings.KeyboardSupport = true;
            gridSettings.SettingsBehavior.AllowGroup = false;
            gridSettings.SettingsBehavior.AllowSort = true;
            gridSettings.SettingsBehavior.AllowDragDrop = false;
            gridSettings.SettingsBehavior.EnableRowHotTrack = true;
            gridSettings.SettingsBehavior.AllowFocusedRow = true;
            gridSettings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
            gridSettings.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // GridViews
    }
}