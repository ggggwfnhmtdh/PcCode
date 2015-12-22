using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolSpace
{

    public  class ProCMD
    {
        public const int USB_CMD_NOP = 0x00;
        public const int USB_CMD_GET_RAW_DATA = 0x01;
        public const int USB_CMD_FW_UPDATE = 0x02;
        public const int USB_CMD_REG_RW = 0x03;
        public const int USB_CMD_HW_CFG = 0x04;
        public const int USB_CMD_EVK_VERSION = 0x05;
        public const int USB_CMD_FLASH_RW = 0x06;
        public const int USB_CMD_ORDER = 0x07;
        public const int USB_CMD_GET_KEY_DATA = 0x09;
        public const int USB_CMD_SYSTEM_TEST = 0x0B;
        public const int USB_CMD_ARM_FW_UPDATE = 0x0C;
        public const int USB_CMD_SYSTEM_VERFY = 0x0D;
        public const int USB_CMD_JIG_STATUS = 0x0E;
        public const int USB_CMD_ARM_NOTICE_PC = 0x44;

        public const int USB_SPEC_RAW_DATA_AUTO = 0x00;
        public const int USB_SPEC_RAW_DATA_MANUAL = 0x01;
        public const int USB_SPEC_REG_WRITE = 0x00;
        public const int USB_SPEC_REG_READ = 0x01;
        public const int USB_SPEC_REG_ACCESS = 0x02;
        public const int USB_SPEC_STATUS_READ = 0x03;
        public const int USB_SPEC_JIG_GPIO_WRITE = 0x04;
        public const int USB_SPEC_POWER_TEST = 0x01;
        public const int USB_SPEC_PIXEL_TEST = 0x02;
        public const int USB_SPEC_PIN_TEST = 0x03;
        public const int USB_SPEC_POWER_REF_INIT = 0x04;
        public const int USB_SPEC_POWERTEST_READY = 0x05;
        public const int USB_SPEC_KEYTEST_AUTO = 0x06;
        public const int USB_SPEC_KEYTEST_TOUCH = 0x07;
        public const int USB_SPEC_RAMTEST = 0x08;
        public const int USB_SPEC_ENTER_BOOT = 0x01;
        public const int USB_SPEC_FLASH_WRITE = 0x00;
        public const int USB_SPEC_FLASH_READ = 0x01;
        public const int USB_SPEC_FLASH_ERASE = 0x02;
        public const int USB_SPEC_ORDER_RESET_PIN = 0x00;
        public const int USB_SPEC_ORDER_RESET_QUICK = 0x01;
        public const int USB_SPEC_ORDER_RESET_SLOW = 0x02;
        public const int USB_SPEC_ORDER_RESET_CPU = 0x03;
        public const int USB_SPEC_ORDER_SLEEP_SPI_CSN_SEL = 0x04;
        public const int USB_SPEC_ORDER_SLEEP_SPI_CSN = 0x05;
        public const int USB_SPEC_ORDER_SLEEP_SPI_CMD = 0x06;
        public const int USB_SPEC_ORDER_SLEEP_INT = 0x07;
        public const int USB_SPEC_ORDER_INTERRUPT_MONITER = 0x08;
        public const int USB_SPEC_ORDER_SEND_CONFIG = 0x09;
        public const int USB_SPEC_ORDER_ARM_VOLTAGE = 0x10;
        public const int USB_SPEC_ORDER_PIXEL_START = 0x11;
        public const int USB_SPEC_ORDER_FINGER_DETECT = 0x13;
        public const int USB_SPEC_OTP_READ = 0x0A;

        public const int USB_SPEC_FW_UPDATE_DSP_ISP = 0x00;
        public const int USB_SPEC_FW_UPDATE_SS51_BLOCK0 = 0x01;
        public const int USB_SPEC_FW_UPDATE_SS51_BLOCK1 = 0x02;
        public const int USB_SPEC_FW_UPDATE_SS51_BLOCK2 = 0x03;
        public const int USB_SPEC_FW_UPDATE_SS51_BLOCK3 = 0x04;
        public const int USB_SPEC_FW_UPDATE_DSP_CODE = 0x05;
        public const int USB_SPEC_FW_UPDATE_BOOT_CODE = 0x06;
        public const int USB_SPEC_FW_UPDATE_DSP_ISP_9P = 0x07;
        public const int USB_SPEC_FW_UPDATE_PATCH_BLOCK0 = 0x08;
        public const int USB_SPEC_FW_UPDATE_PATCH_BLOCK1 = 0x09;
        public const int USB_SPEC_FW_UPDATE_DSP_CODE_9P = 0x0A;
        public const int USB_SPEC_FW_UPDATE_GF11 = 0x0B;
        public const int USB_SPEC_FW_UPDATE_MILAN_TS = 0x0C;

        public const int OUT_INDEX_USB_CMD = 0;
        public const int OUT_INDEX_SPEC = 1;
        public const int OUT_INDEX_TOTAL_H = 2;
        public const int OUT_INDEX_TOTAL_L = 3;
        public const int OUT_INDEX_CURR_H = 4;
        public const int OUT_INDEX_CURR_L = 5;
        public const int OUT_INDEX_LEN = 6;
        public const int OUT_INDEX_DATA = 8;


        public const int OUT_INDEX_REG_H = 2;
        public const int OUT_INDEX_REG_L = 3;
        public const int OUT_INDEX_FRAME = 4;
        public const int OUT_INDEX_FRAME_CNT = 5;
        public const int OUT_INDEX_RW_LEN_H = 4;
        public const int OUT_INDEX_RW_LEN_L = 5;

        public const int IN_INDEX_USB_CMD = 0;
        public const int IN_INDEX_SPEC = 1;
        public const int IN_INDEX_STATUS = 2;
        public const int IN_INDEX_TOTAL_H = 3;
        public const int IN_INDEX_TOTAL_L = 4;
        public const int IN_INDEX_CURR_H = 5;
        public const int IN_INDEX_CURR_L = 6;
        public const int IN_INDEX_LEN = 7;
        public const int IN_INDEX_DATA = 8;


        public const int IN_DATA_LEN_MAX = 56;
        public const int OUT_DATA_LEN_MAX = 56;
        public const int OUT_CMD_LEN_MAX = 8;
        public const int USB_ACK_OK = 0;
        public const int USB_ACK_ERR = 1;

        public const  int FRAME_DATA_SIZE = 56; // ????????
        public const  int USB_PACKET_SIZE = 64; // ????????

        public const int CMD_ACK_WATITING = 0;
        public const int CMD_ACK_RECEIVE = 1;
        public const int CMD_ACK_STATUS_OK = 0;
        public const int CMD_ACK_STATUS_ERROR = 1;

        public const int GPIOA = 0;
        public const int GPIOB = 1;
        public const int GPIOC = 2;
        public const int GPIOD = 3;
        public const int GPIOE = 4;
        public const int JIG_LED0_R = (GPIOC << 4 | 12);
        public const int JIG_LED0_G = (GPIOC << 4 | 10);
        public const int JIG_LED1_R = (GPIOA << 4 | 8);
        public const int JIG_LED1_G = (GPIOC << 4 | 8);
        public const int JIG_LED2_R = (GPIOD << 4 | 6);
        public const int JIG_LED2_G = (GPIOD << 4 | 5);
        public const int JIG_LED3_R = (GPIOD << 4 | 4);
        public const int JIG_LED3_G = (GPIOD << 4 | 2);
        public const int JIG_BUZZ = (GPIOA << 4 | 9);
        public const int JIG_SITM = (GPIOD << 4 | 15);
        public const int JIG_BUTTION_START = (GPIOD << 4 | 11);
        public const int JIG_BUZZ_RESET = (GPIOD << 4 | 13);
        public const int JIG_STIM_SENSE = (GPIOC << 4 | 9);
        public const int JIG_BUTTION_EMERGENCY = (GPIOC << 4 | 11);
        public const int JIG_PHOTO_SENSE = (GPIOD << 4 | 7);

        public const int GF12_FIRMWARE_LEN = 2560;   
        public const int GF12_CONFIGFILE_LEN = 110;

        public const int RAWDATA_MAX_BUFFER = 96*96;
    }

    public class m_cmdAck
    {
        public static byte cmd0;
        public static byte cmd1;
        public static byte cmdStatus;
        public static byte[] cmdData = new byte[20 * 1024];
        public static byte ackIsProc;
    } 
}

