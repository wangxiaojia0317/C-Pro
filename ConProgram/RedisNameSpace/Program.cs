using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using ServiceStack.Redis;
using ServiceStack.Caching;
using ServiceStack.Messaging;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;

namespace RedisNameSpace
{

   
    class Program
    {
        static void TestProperity<T>(T t,PropertyInfo info)where T :class,new ()
        {

            List<string> _list = new List<string> { "System.Collections.Generic.List`1[System.Int32]", "System.Collections.Generic.List`1[System.string]" };
           
            List<string> list = new List<string> { "PartLocationObj", "CrossRunParamObj" };
            if (list.Contains(info.Name))
            {
                switch (info.Name)
                {
                    case "PartLocationObj":
                        PartWithLocation  pwl = t as PartWithLocation;
                        PropertyInfo[] infoObj = pwl.PartLocationObj.GetType().GetProperties();
                        foreach (var item in infoObj)
                        {
                            Console.WriteLine(item.GetValue(pwl.PartLocationObj));
                        }

                        break;

                    case "CrossRunParamObj":
                        TransitCenterCrossLocalParam obj = t as TransitCenterCrossLocalParam;
                        PropertyInfo[] infoParam = obj.CrossRunParamObj.GetType().GetProperties();

                        foreach (var item in infoParam)
                        {
                            if (_list.Contains(item.GetValue(obj.CrossRunParamObj).ToString()))
                            {
                                switch (item.GetValue(obj.CrossRunParamObj).ToString())
                                {
                                    case "System.Collections.Generic.List`1[System.Int32]":
                                        List<int> dataInt = (List<int>)item.GetValue(obj.CrossRunParamObj);
                                        foreach (var a in dataInt)
                                        {
                                            Console.WriteLine(a);
                                        }
                                        break;

                                    case "System.Collections.Generic.List`1[System.string]":
                                        List<string> dataString = (List<string>)item.GetValue(obj.CrossRunParamObj);
                                        foreach (var a in dataString)
                                        {
                                            Console.WriteLine(a);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine(item.Name+":"+item.GetValue(obj.CrossRunParamObj));
                            }

                        }
                        
                        break;


                }
            }
            else
            {
                Console.WriteLine(info.GetValue(t));
            }
        }

        static void Main(string[] args)
        {

            using (RedisClient client=new RedisClient("127.0.0.1",6379))
            {
                var s =   client.As<PartWithLocation>().GetAll().GroupBy(x=>x.PartLocationObj.Index);
            }

            Console.Read();
        }

        public static List<string> ObjList = new List<string> {
            "PartLocationObj",
            "CrossRunParamObj" ,
            "FirstJobQueue",
            "SecondJobQueue",
            "InForkTransferMsgObj",
            "OutForkTransferMsgObj",
            "FromPosition",
            "ToPosition" ,
            "ForkTransferMsgObj",
            "QueueObj",
            "hashObj",
            "HashtableObj",
            "ReIssueOrderListItem",
            "ReIssueOrderList",
          "Data"
        };
        public static List<string> colletionList = new List<string> {
             "SourcePartIDSibling",
             "RemedyPartIDs",
             "RemedyBatchIDs",
             "Data",//--
             "SecondJobQueue",//
             "FirstJobQueue",//
             "QueueObj",
             "PreviousUnitSendingCmdQueue",
             "NextUnitReceivingCmdQueue",
             "RequestList",
             "SourcePartIDSibling",
             "TotalPlannedBoardPartsIDList",
             "TotalPlannedLeftoverPartsIDList",
             "NeedOutputBoardPartIDList",
             "OutputBoardPartIDList",
             "ListenedEmptyBoardPartIDList",
             "ListenedBadBoardPartIDList",
             "ListenedBadBoardPartIDList2",
             "ListenedEmptyLeftoverPartIDList",
             "ListenedBadLeftoverPartIDList",
             "ListenedBadLeftoverPartIDList2",
             "NeededLeftoverPartList",
             "NeededLeftoverPartList_Have",
             "NeededLeftoverPartList_HaveNot",
             "OutputNeededLeftoverPartList_Have",
             "ReIssueOrderIDList",
             "ReIssueOrderList",//----
             "RemedyBoardPartNotInNeedList",
             "RequireToOutputBoardPartIDList",
             "RequireToOutputBoardPartIDList_Success",
             "ResponseToOutputBoardPartIDList_Success",
             "RequireToOutputBoardPartIDList_Fail",
             "RequireToOutputLeftoverPartIDList",
             "RequireToOutputLeftoverPartIDList_Success",
             "ResponseToOutputLeftoverPartIDList_Success",
             "RequireToOutputLeftoverPartIDList_Fail",
             "Data",
            
        };

        public static List<string> colletionDouList = new List<string> {
            "EnterScanPartInforSequenceList"
        };


       static StringBuilder builder = new StringBuilder();
       
        static string Test(object t)
        {
            Type type = t.GetType();
            PropertyInfo[] info = type.GetProperties();
            foreach (var item in info)
            {
                if (item.GetValue(t)==null)
                {
                    continue;
                }
                if (ObjList.Contains(item.Name)&& colletionList.Contains(item.Name))
                {
                    builder.Append("\n");
                    builder.Append(item.Name + ":");
                    switch (item.Name)
                    {
                        case "Data":
                            List<int> data = (List<int>)item.GetValue(t);
                            foreach (var unit in data)
                            {
                                builder.Append(unit+",");
                            }
                            break;

                        case "FirstJobQueue":
                            List<FirstJobQueue> firstList = (List<FirstJobQueue>)item.GetValue(t);
                            foreach (var unit in firstList)
                            {
                                Test(unit);
                            }
                            break;

                        case "SecondJobQueue":
                            List<SecondJobQueue> secondList = (List<SecondJobQueue>)item.GetValue(t);
                            foreach (var unit in secondList)
                            {
                                Test(unit);
                            }
                            break;

                        case "ReIssueOrderList":
                            List<ReIssueOrderListItem> reissList= (List<ReIssueOrderListItem>)item.GetValue(t);
                            foreach (var unit in reissList)
                            {
                                Test(unit);
                            }
                            break;
                    }
                }
                else if (ObjList.Contains(item.Name))
                {
                    Test(item.GetValue(t));
                }
                else if (colletionList.Contains(item.Name))
                {
                    List<string> data = (List<string>)item.GetValue(t);
                   
                    builder.Append(item.Name + "\n");
                    foreach (var unit in data)
                    {
                        builder.Append(unit + "\n");
                    }
                }
                else if (colletionDouList.Contains(item.Name))
                {
                    List<List<string>> data1 = item.GetValue(t) as List<List<string>>;

                    foreach (var one in data1)
                    {
                        builder.Append("[");
                        foreach (var two in one)
                        {
                            builder.Append(two);
                            builder.Append(",");
                        }
                        builder.Append("]");
                        builder.Append("\n");
                    }
                }
                else if (info.First().Name != "ID"&&item.Name=="ID")
                {
                    builder.Append(item.Name + ":");
                    List<int> data = (List<int>)item.GetValue(t);
                    foreach (var unit in data)
                    {
                        builder.Append(unit + ",");
                    }
                }  
                else
                {
                    builder.Append(item.Name + ":"+ item.GetValue(t)+"\n");
                }
            }
           
            return builder.ToString();

        }


     
    }

    #region Json实体类

    #region PartWithLocation
    public class PartLocationObj
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Z
        /// </summary>
        public int Z { get; set; }
        /// <summary>
        /// LeftOrRight
        /// </summary>
        public int LeftOrRight { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// PartStep
        /// </summary>
        public int PartStep { get; set; }
        /// <summary>
        /// /Date(1539323974534+0800)/
        /// </summary>
        public string CalculateTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string EnterTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string ExitTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string LeaveTime { get; set; }
    }

    public class PartWithLocation
    {
        /// <summary>
        /// 1ACDp6019143
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// PartLocationObj
        /// </summary>
        public PartLocationObj PartLocationObj { get; set; }
        /// <summary>
        /// 1ACDp6019143
        /// </summary>
        public string BoardPartID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// BoardPart
        /// </summary>
        public string PartType { get; set; }
        /// <summary>
        /// Calculate
        /// </summary>
        public string PartStatus { get; set; }
    }

    #endregion

    #region ReissueOrderInfor
    #region ReissueOrderInfor
    public class ReIssueOrderInfor
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourcePartID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceBatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SourcePartIDSibling { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceRelationKind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OutputType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PartType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PartStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RemedyPartIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RemedyBatchIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorDate { get; set; }
    }
    #endregion

    #endregion

    #region TransitCenterCrossLocalParam
    public class CrossRunParamObj
    {
        /// <summary>
        /// StartAddress
        /// </summary>
        public int StartAddress { get; set; }
        /// <summary>
        /// TaskLock
        /// </summary>
        public int TaskLock { get; set; }
        /// <summary>
        /// TaskIndex
        /// </summary>
        public int TaskIndex { get; set; }
        /// <summary>
        /// TaskResult
        /// </summary>
        public int TaskResult { get; set; }
        /// <summary>
        /// TaskError
        /// </summary>
        public int TaskError { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// From
        /// </summary>
        public int From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public int To { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public List<int> ID { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<int> Data { get; set; }
    }

    public class TransitCenterCrossLocalParam
    {
        /// <summary>
        /// 1ACDp6019116
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Enter
        /// </summary>
        public string Step { get; set; }
        /// <summary>
        /// REACH_CROSS_TRANSITCENTER
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// CrossRunParamObj
        /// </summary>
        public CrossRunParamObj CrossRunParamObj { get; set; }
    }

    #endregion

    #region TransitCenterForkLocalParam

    public class FirstJobQueue
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6035020
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// MaxBoardPartType
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
    }

    public class SecondJobQueue
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6021116,1ACDp6021124
        /// </summary>
        public string ExtraInformationInBuff { get; set; }
        /// <summary>
        /// 1ACDp6021063,1ACDp6021085,1ACDp6021104,1ACDp6021116,1ACDp6021124,1ACDp6021141,1ACDp6021161,1ACDp6022005,1ACDp6022029
        /// </summary>
        public string ExtraInformationInPackage { get; set; }
        /// <summary>
        /// 1ACDp6021116
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// Package
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
        /// <summary>
        /// none
        /// </summary>
        public string Route { get; set; }
    }


    public class ForkTransferMsgObj
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6022098
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// MaxBoardPartType
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
    }

    public class InForkTransferMsgObj
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6022098
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// MaxBoardPartType
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
    }



    public class OutForkTransferMsgObj
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6021116,1ACDp6021124
        /// </summary>
        public string ExtraInformationInBuff { get; set; }
        /// <summary>
        /// 1ACDp6021063,1ACDp6021085,1ACDp6021104,1ACDp6021116,1ACDp6021124,1ACDp6021141,1ACDp6021161,1ACDp6022005,1ACDp6022029
        /// </summary>
        public string ExtraInformationInPackage { get; set; }
        /// <summary>
        /// 1ACDp6021116
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// Package
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
        /// <summary>
        /// none
        /// </summary>
        public string Route { get; set; }
    }

    public class TransitCenterForkLocalParam
    {
        /// <summary>
        /// TRANSITCENTER_FORK_0
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// SENDING_FINISH_IND
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// TRANSITCENTER_RACK_0
        /// </summary>
        public string CurrentWorkingNextUnitName { get; set; }
        /// <summary>
        /// FirstJobQueue
        /// </summary>
        public List<FirstJobQueue> FirstJobQueue { get; set; }
        /// <summary>
        /// SecondJobQueue
        /// </summary>
        public List<SecondJobQueue> SecondJobQueue { get; set; }
        /// <summary>
        /// TRANSITCENTER_RACK_0
        /// </summary>
        public string FirstJobQueueNextUnitName { get; set; }
        /// <summary>
        /// TRANSITCENTER_OUTPUTPIPE_0
        /// </summary>
        public string SecondJobQueueNextUnitName { get; set; }
        /// <summary>
        /// FirstJobQueueNextUnitReadyPending
        /// </summary>
        public bool firstJobQueueNextUnitReadyPending { get; set; }
        /// <summary>
        /// SecondJobQueueNextUnitReadyPending
        /// </summary>
        public bool secondJobQueueNextUnitReadyPending { get; set; }
        /// <summary>
        /// ForkTransferMsgObj
        /// </summary>
        public ForkTransferMsgObj ForkTransferMsgObj { get; set; }
        /// <summary>
        /// InForkTransferMsgObj
        /// </summary>
        public InForkTransferMsgObj InForkTransferMsgObj { get; set; }
        /// <summary>
        /// OutForkTransferMsgObj
        /// </summary>
        public OutForkTransferMsgObj OutForkTransferMsgObj { get; set; }
        /// <summary>
        /// TaskIndex
        /// </summary>
        public int TaskIndex { get; set; }
        /// <summary>
        /// HaveReceivedGetPartCmdExeResult
        /// </summary>
        public string CmdStatus { get; set; }
    }

    #endregion

    #region TransitCenterInputPipeLocalParam
    public class FromPosition
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Z
        /// </summary>
        public int Z { get; set; }
        /// <summary>
        /// LeftOrRight
        /// </summary>
        public int LeftOrRight { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// PartStep
        /// </summary>
        public int PartStep { get; set; }
        /// <summary>
        /// /Date(1539323941252+0800)/
        /// </summary>
        public string CalculateTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string EnterTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string ExitTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string LeaveTime { get; set; }
    }

    public class ToPosition
    {
        /// <summary>
        /// X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Z
        /// </summary>
        public int Z { get; set; }
        /// <summary>
        /// LeftOrRight
        /// </summary>
        public int LeftOrRight { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// PartStep
        /// </summary>
        public int PartStep { get; set; }
        /// <summary>
        /// /Date(1539323899626+0800)/
        /// </summary>
        public string CalculateTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string EnterTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string ExitTime { get; set; }
        /// <summary>
        /// /Date(-28800000-0000)/
        /// </summary>
        public string LeaveTime { get; set; }
    }

    public class QueueObj
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6035020
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// MaxBoardPartType
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
    }

    public class TransitCenterInputPipeLocalParam
    {
        /// <summary>
        /// TRANSITCENTER_INPUTPIPE_0
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// HaveSendCmd
        /// </summary>
        public string CmdStatus { get; set; }
        /// <summary>
        /// QueueObj
        /// </summary>
        public List<QueueObj> QueueObj { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// MaxInputLength
        /// </summary>
        public int MaxInputLength { get; set; }
        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// MaxCount
        /// </summary>
        public int MaxCount { get; set; }
    }

    #endregion

    #region TransitCenterJackLocalParam
    public class TransitCenterJackLocalParam
    {
        /// <summary>
        /// TRANSITCENTER_JACK_0
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Default
        /// </summary>
        public string CmdStatus { get; set; }
        /// <summary>
        /// TaskIndex
        /// </summary>
        public int TaskIndex { get; set; }
        /// <summary>
        /// Direct
        /// </summary>
        public int Direct { get; set; }
    }



    #endregion

    #region TransitCenterOutputPipeLocalParam

    public class TransitCenterOutputPipeLocalParam
    {
        /// <summary>
        /// TRANSITCENTER_OUTPUTPIPE_5
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// IDLE
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// PreviousUnitSendingCmdQueue
        /// </summary>
        public List<string> PreviousUnitSendingCmdQueue { get; set; }
        /// <summary>
        /// NextUnitReceivingCmdQueue
        /// </summary>
        public List<string> NextUnitReceivingCmdQueue { get; set; }
        /// <summary>
        /// TRANSITCENTER_FORK_5
        /// </summary>
        public string CurrentWorkingPreviousUnitName { get; set; }
        /// <summary>
        /// RefuseToReceive
        /// </summary>
        public bool RefuseToReceive { get; set; }
        /// <summary>
        /// QueueObj
        /// </summary>
        public List<QueueObj> QueueObj { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// MaxLength
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// Laps
        /// </summary>
        public int Laps { get; set; }
        /// <summary>
        /// StartLaps
        /// </summary>
        public int StartLaps { get; set; }
        /// <summary>
        /// EndLaps
        /// </summary>
        public int EndLaps { get; set; }
        /// <summary>
        /// XOutputMaxValidPosition
        /// </summary>
        public int XOutputMaxValidPosition { get; set; }
        /// <summary>
        /// Package
        /// </summary>
        public string OutputPipeType { get; set; }
    }

    #endregion

    #region TransitCenterOutputPipeManagerLocalParam
    /// <summary>
    /// 目前处于待定的状态，这里有一个hashtable导致的字段出现的数字的效果，目前商定，随后再说
    /// </summary>
    public class hashObj
    {
        /// <summary>
        /// FromPosition
        /// </summary>
        public FromPosition FromPosition { get; set; }
        /// <summary>
        /// ToPosition
        /// </summary>
        public ToPosition ToPosition { get; set; }
        /// <summary>
        /// PartCountInOutPipe
        /// </summary>
        public int PartCountInOutPipe { get; set; }
        /// <summary>
        /// PartCountInBuff
        /// </summary>
        public int PartCountInBuff { get; set; }
        /// <summary>
        /// PartCountInPackage
        /// </summary>
        public int PartCountInPackage { get; set; }
        /// <summary>
        /// HasSiblingFlag
        /// </summary>
        public bool HasSiblingFlag { get; set; }
        /// <summary>
        /// 1ACDp6021124
        /// </summary>
        public string ExtraInformationInOutPipe { get; set; }
        /// <summary>
        /// 1ACDp6021116,1ACDp6021124
        /// </summary>
        public string ExtraInformationInBuff { get; set; }
        /// <summary>
        /// 1ACDp6021063,1ACDp6021085,1ACDp6021104,1ACDp6021116,1ACDp6021124,1ACDp6021141,1ACDp6021161,1ACDp6022005,1ACDp6022029
        /// </summary>
        public string ExtraInformationInPackage { get; set; }
        /// <summary>
        /// 1ACDp6021124
        /// </summary>
        public string BoardID { get; set; }
        /// <summary>
        /// 1ACDp5605000
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// Package
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// IsGoOuting
        /// </summary>
        public bool IsGoOuting { get; set; }
        /// <summary>
        /// none
        /// </summary>
        public string Route { get; set; }
    }

    public class HashtableObj
    {
        /// <summary>
        /// 0
        /// </summary>
        public hashObj obj { get; set; }
    }

    public class TransitCenterOutputPipeManagerLocalParam
    {
        /// <summary>
        /// TRANSITCENTER_OUTPUTPIPE_MANAGER
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// HashtableObj
        /// </summary>
        public HashtableObj HashtableObj { get; set; }
        /// <summary>
        /// RequestList
        /// </summary>
        public List<string> RequestList { get; set; }
        /// <summary>
        /// DownRetainedParts
        /// </summary>
        public int DownRetainedParts { get; set; }
        /// <summary>
        /// UpRetainedParts
        /// </summary>
        public int UpRetainedParts { get; set; }
        /// <summary>
        /// IDLE
        /// </summary>
        public string DownRouteWorkStatus { get; set; }
        /// <summary>
        /// IDLE
        /// </summary>
        public string UpRouteWorkStatus { get; set; }
        /// <summary>
        /// 000000
        /// </summary>
        public string InstructValue { get; set; }
    }


    #endregion

    #region BatchOutputStatus
    public class ReIssueOrderListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourcePartID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceBatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SourcePartIDSibling { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceRelationKind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OutputType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PartType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PartStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RemedyPartIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RemedyBatchIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OperatorDate { get; set; }
    }
    public class BatchOutputStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeletedBatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> TotalPlannedBoardPartsIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> TotalPlannedLeftoverPartsIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> NeedOutputBoardPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> OutputBoardPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedEmptyBoardPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedBadBoardPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedBadBoardPartIDList2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedEmptyLeftoverPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedBadLeftoverPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ListenedBadLeftoverPartIDList2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> NeededLeftoverPartList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> NeededLeftoverPartList_Have { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> NeededLeftoverPartList_HaveNot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> OutputNeededLeftoverPartList_Have { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ReIssueOrderIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ReIssueOrderListItem> ReIssueOrderList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RemedyBoardPartNotInNeedList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputBoardPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputBoardPartIDList_Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ResponseToOutputBoardPartIDList_Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputBoardPartIDList_Fail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputLeftoverPartIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputLeftoverPartIDList_Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ResponseToOutputLeftoverPartIDList_Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> RequireToOutputLeftoverPartIDList_Fail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstEnterTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastEnterTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstExitTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastExitTime { get; set; }
    }
    #endregion

    #region TransitCenterScanLocalParam 18个扫码枪的状态
    public class TransitCenterScanLocalParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CurrentScanId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CmdStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List<string>> EnterScanPartInforSequenceList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExitScanTotalCountCircle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExitScanCountCircle { get; set; }
    }

    #endregion


    #region LeftOverPart
    public class LabelInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string PartID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsNormal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double PointX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PointY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Angle { get; set; }
    }

    public class Repository
    {
        /// <summary>
        /// 
        /// </summary>
        public string __type { get; set; }
    }

    public class LeftoverPart
    {
        /// <summary>
        /// 
        /// </summary>
        public string rect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsMatched { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UsedBatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutSizeX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutSizeY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutSizeZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Rotate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutRotation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LastRotation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RollOver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Kind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LabelInfo labelInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> MatIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> MatList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTimeOfDrill { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTimeOfDrill { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> DrillIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> DrillList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTimeOfGroove { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTimeOfGroove { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GrooveIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GrooveList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> PointList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingPartList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsPrepared { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingBoardPartIDListWithReIssueOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingBoardPartListWithReIssueOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CutmapID { get; set; }
        /// <summary>
        /// 30mm木纹饰面板
        /// </summary>
        public string PlateID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EdgeValue1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EdgeValue2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int edgeMachine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Repository Repository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double SizeX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double SizeY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SizeZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationZ { get; set; }
    }
    #endregion


    #region BoardPart
    public class BoardPart
    {
        /// <summary>
        /// 
        /// </summary>
        public double CutSizeX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double CutSizeY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutSizeZ { get; set; }
        /// <summary>
        /// 30mm木纹饰面板
        /// </summary>
        public string MatMaterial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TopSurface { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BottomSurface { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Rotate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutRotation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LastRotation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RollOver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Kind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EdgeValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LabelInfo labelInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> MatIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> MatList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTimeOfDrill { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTimeOfDrill { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> DrillIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> DrillList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTimeOfGroove { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTimeOfGroove { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GrooveIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GrooveList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> PointList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingPartList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RelationID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsPrepared { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastReIssueOrderBoardPartID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingBoardPartIDListWithReIssueOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> SiblingBoardPartListWithReIssueOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GetHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BatchName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CutmapID { get; set; }
        /// <summary>
        /// 30mm木纹饰面板
        /// </summary>
        public string PlateID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EdgeValue1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EdgeValue2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int edgeMachine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Repository Repository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double SizeX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double SizeY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SizeZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RotationZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID { get; set; }
    }
    #endregion

    #region Batch
    public class Batch
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BatchIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductionStartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductionEndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductionStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ArticleIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ArticleList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> CuttingMapIDList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> CuttingMapList { get; set; }
    }

    #endregion


    #endregion


}
