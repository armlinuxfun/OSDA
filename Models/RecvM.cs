﻿using Microsoft.Win32;
using OSerialPort.Interface;
using OSerialPort.ViewModels;
using System;
using System.IO;
using System.IO.Ports;

namespace OSerialPort.Models
{
    public class RecvModel : MainWindowBase
    {
        public string DataRecePath = null;
        /// <summary>
        /// 实现接收区数据超过32MB时，自动清空接收控件中的数据
        /// </summary>
        public int RecvDataDeleteCount = 1;

        public int _RecvDataCount;
        public int RecvDataCount
        {
            get
            {
                return _RecvDataCount;
            }
            set
            {
                if (_RecvDataCount != value)
                {
                    _RecvDataCount = value;
                    RaisePropertyChanged(nameof(RecvDataCount));
                }
            }
        }

        /* 接收区Header中的 [保存中/已停止] 字符串 */
        public string _RecvAutoSave;
        public string RecvAutoSave
        {
            get
            {
                return _RecvAutoSave;
            }
            set
            {
                if (_RecvAutoSave != value)
                {
                    _RecvAutoSave = value;
                    RaisePropertyChanged(nameof(RecvAutoSave));
                }
            }
        }

        public string _RecvHeader;
        public string RecvHeader
        {
            get
            {
                return _RecvHeader;
            }
            set
            {
                if (_RecvHeader != value)
                {
                    _RecvHeader = value;
                    RaisePropertyChanged(nameof(RecvHeader));
                }
            }
        }

        public ITextBoxAppend _RecvData;
        public ITextBoxAppend RecvData
        {
            get
            {
                return _RecvData;
            }
            set
            {
                if (_RecvData != value)
                {
                    _RecvData = value;
                    RaisePropertyChanged(nameof(RecvData));
                }
            }
        }

        /* 辅助区 - 十六进制接收 */
        public bool _HexRecv;
        public bool HexRecv
        {
            get
            {
                return _HexRecv;
            }
            set
            {
                if (_HexRecv != value)
                {
                    _HexRecv = value;
                    RaisePropertyChanged(nameof(HexRecv));
                }
            }
        }

        /* 辅助区 - 保存接收 */
        public bool _SaveRecv;
        public bool SaveRecv
        {
            get
            {
                return _SaveRecv;
            }
            set
            {
                if (_SaveRecv != value)
                {
                    _SaveRecv = value;
                    RaisePropertyChanged(nameof(SaveRecv));
                }

                if (SaveRecv == true)
                {
                    DepictInfo = "接收数据默认保存在程序基目录，可以点击路径选择操作更换";
                }
                else
                {
                    DepictInfo = "串行端口调试助手";
                    RecvAutoSave = "已停止";

                }
            }
        }

        public void RecvPath()
        {
            SaveFileDialog ReceDataSaveFileDialog = new SaveFileDialog
            {
                Title = "接收数据路径选择",
                FileName = string.Format("{0}", DateTime.Now.ToString("yyyyMMdd")),
                Filter = "文本文件|*.txt"
            };

            if (ReceDataSaveFileDialog.ShowDialog() == true)
            {
                DataRecePath = ReceDataSaveFileDialog.FileName;
            }
        }

        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _SerialPort = (SerialPort)sender;

            int _bytesToRead = _SerialPort.BytesToRead;
            byte[] recvData = new byte[_bytesToRead];

            _SerialPort.Read(recvData, 0, _bytesToRead);

            if (HexRecv)
            {
                foreach (var tmp in recvData)
                {
                    RecvData.Append(string.Format("{0:X2} ", tmp));
                }
            }
            else
            {
                RecvData.Append(_SerialPort.Encoding.GetString(recvData));
            }

            if (SaveRecv)
            {
                RecvAutoSave = "保存中";

                SaveRecvData(_SerialPort.Encoding.GetString(recvData));
            }
            else
            {
                RecvAutoSave = "已停止";
            }

            RecvDataCount += recvData.Length;

            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]";

            if(RecvDataCount > (32768 * RecvDataDeleteCount))
            {
                RecvData.Delete();   /* 32MB */

                RecvDataDeleteCount += 1;
            }
        }

        public async void SaveRecvData(string ReceData)
        {
            try
            {
                if (DataRecePath == null)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\");

                    using (StreamWriter DefaultReceDataPath = new StreamWriter(
                        AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                        true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecePath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
            }
            catch
            {
                DepictInfo = "接收数据保存失败";
            }
        }

        public void RecvDataContext()
        {
            RecvData = new IClassTextBoxAppend();
            RecvDataCount = 0;
            RecvAutoSave = "已停止";
            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]";

            HexRecv = false;
            SaveRecv = false;
        }
    }
}
