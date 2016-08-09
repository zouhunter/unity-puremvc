using UnityEngine;
using System.Collections;
public enum NotiConst {
    Nil,
    /// <summary>
    /// Controller
    /// </summary>
    XMLProjectLoad,
    SceneLoadCtrl,
    SqliteSaveCtrl,
    ExperienceLoadCtrl,

    /// <summary>
    /// View
    /// </summary>
    ThreeViewShow,
    ProjectSettingShow,
    PlayerHelpShow,
    ToolPanelShow,

    SliderValue,
    SceneLoadOper,

    //动态信息
    Information_Area,
    Information_State,
    Information_Time,
    Information_Speed,

    RunTimeInfomaion,
    #region 实验配制信息的加载与使用
    //动态信息加载提示
    ExperienceBaseLoad,
    ExperienceBaseUse,
    PresentationDataLoad,
    PresentationDataUse,
    QuestionLoad,
    QuestionUse,
    //元素加载 
    ElementDataLoad,
    //实验步骤
    ProcedureInfoLoad,
    #endregion

    //实验控制
    StapChange,

}
