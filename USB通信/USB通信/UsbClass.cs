using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using LibUsbDotNet;
using LibUsbDotNet.Main;
namespace UsbProtecolSpace
{
    class UsbClass
    {
        public  int UsbDevicePID;
        public  int UsbDeviceVID;
        public UsbClass(int i, int j)
        {
            UsbDevicePID = i;
            UsbDeviceVID = j;
        }
        public UsbRegDeviceList GetAllDevice()
        {

            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            UsbDeviceFinder usbFinder = new UsbDeviceFinder(UsbDevicePID, UsbDeviceVID);
            UsbRegDeviceList usbDevices = new UsbRegDeviceList();
            usbDevices = allDevices.FindAll(usbFinder);
            return usbDevices;
        }

        public static UsbRegistry UsbFilter(UsbRegDeviceList usbDevices, String DevPro, String ConTent)
        {
            int i = 0;
            for (i = 0; i < usbDevices.Count; i++)
            {
                UsbRegistry UsbDev = usbDevices[i];
                if (UsbDev.DeviceProperties[DevPro].ToString() == ConTent)
                   return UsbDev;
            }
                return null;                
        }
    }
}

//    UsbRegistry UsbDev = usbDevices[i];
//    Debug.Text += "Usb??" + i.ToString() + "??" + "\r\n";
//    Debug.Text += UsbDev.SymbolicName + "\r\n";
//    Debug.Text += UsbDev.Device.Info.SerialString + "\r\n";
//   Debug.Text += "LocationInformation:" + UsbDev.DeviceProperties["LocationInformation"] + "\r\n";
//    Debug.Text += "ClassGuid:" + UsbDev.DeviceProperties["ClassGuid"] + "\r\n";
//    Debug.Text += "FriendlyName:" + UsbDev.DeviceProperties["FriendlyName"] + "\r\n";
//    Debug.Text += "BusNumber:" + UsbDev.DeviceProperties["BusNumber"] + "\r\n";
//    Debug.Text += "Address:" + UsbDev.DeviceProperties["Address"] + "\r\n";
//    Debug.Text += "HardwareId:" + UsbDev.DeviceProperties["HardwareId"] + "\r\n";
//    Debug.Text += "\r\n";