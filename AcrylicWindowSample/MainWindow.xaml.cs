/*
    Copyright (c) 2016 minami_SC
    URL:https://github.com/sourcechord/FluentWPF
 */






using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using SolutionItems;

namespace AcrylicWindowSample
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public uint GradientColor;
        public int AnimationId;
    }

















    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow 
    {

        private DispatcherTimer timer;
        private Counter dayday;
        

        //処理
        public MainWindow()
        {
            
            
            InitializeComponent();
            timer = CreateTimer();
            dayday = new Counter();
            days.Text = dayday.CountDays();
            timer.Start();
        }

        


        //初期化


        //public override void OnApplyTemplate()
        //{
          //  base.OnApplyTemplate();
            // ウィンドウ背景のぼかし効果を有効にする
            //EnableBlur(this);
        //}


        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal static void EnableBlur(Window win)
        {
            var windowHelper = new WindowInteropHelper(win);

            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            accent.AccentFlags = 2;
            //               ↓の色はAABBGGRRの順番で設定する
            accent.GradientColor = 0x99FFFFFF;  // 60%の透明度が基本

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        //現在時刻を表示
        //https://qiita.com/Kosen-amai/items/88b3d987b41f46ebea4b
        /// <summary>
        /// タイマー生成処理
        /// </summary>
        /// <returns>生成したタイマー</returns>
        private DispatcherTimer CreateTimer()
        {
            // タイマー生成（優先度はアイドル時に設定）
            var t = new DispatcherTimer(DispatcherPriority.SystemIdle);
            var culture = new System.Globalization.CultureInfo("en-US");
            
            
            // タイマーイベントの発生間隔を300ミリ秒に設定
            t.Interval = TimeSpan.FromMilliseconds(300);

            // タイマーイベントの定義
            t.Tick += (sender, e) =>
            {
                // タイマーイベント発生時の処理をここに書く

                // 現在の時分秒をテキストに設定
                nowtimer.Text = DateTime.Now.ToString("MMM d  HH:mm ddd", culture);
                /////nowtimer.Text = DateTime.Now.ToString("yyyy.MMMdd.HH:mm:ss  ddd", culture);
            };

            // 生成したタイマーを返す
            return t;
        }









        
    }
}
