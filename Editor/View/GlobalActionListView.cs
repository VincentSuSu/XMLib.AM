/*
 * 作者：Peter Xiang
 * 联系方式：565067150@qq.com
 * 文档: https://github.com/PxGame
 * 创建时间: 2019/10/28 16:40:09
 */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XMLib.AM
{
    /// <summary>
    /// GlobalActionListView
    /// </summary>
    [Serializable]
    public class GlobalActionListView : IDataView
    {
        public override string title => "全局动作列表";
        public override bool useAre => true;

        private Vector2 scrollPos;

        protected override void OnGUI(Rect rect)
        {
            List<object> configs = win.currentGlobalActions;
            if (null == configs)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            win.globalActionSelectIndex = EditorGUILayoutEx.DrawList(configs, win.globalActionSelectIndex, ref scrollPos, NewAction, ActionEditorUtility.ItemDrawer);
            if (EditorGUI.EndChangeCheck())
            {
                //win.configModification = true;
            }
        }

        private void NewAction(Action<object> adder)
        {
            SelectListWindow.ShowTypeWithAttr<ActionConfigAttribute>(t =>
            {
                object obj = Activator.CreateInstance(t);
                adder(obj);
            });
        }

        public override void OnUpdate()
        {
        }

        public override object CopyData()
        {
            return win.currentGlobalAction;
        }

        public override void PasteData(object data)
        {
            if (data.GetType().IsDefined(typeof(ActionConfigAttribute), true))
            {
                win.currentGlobalActions.Add(data);
                win.globalActionSelectIndex = win.currentGlobalActions.Count - 1;
            }
        }
    }
}