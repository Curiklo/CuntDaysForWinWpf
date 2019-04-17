using System;

namespace SolutionItems

{ 

    public class Counter
    {
	
	
        public String CountDays()
        {
            // タイマー生成（優先度はアイドル時に設定）

            DateTime startday = new DateTime(2015, 10, 01);
            // タイマーイベントの発生間隔を300ミリ秒に設定
            TimeSpan getdays = DateTime.Today - new DateTime(2015, 10, 1);

            // 現在の時分秒をテキストに設定
            days.Text = getdays.ToString("dd");

            // 生成したタイマーを返す
            //return getdays.ToString();
        }
    
    }

}