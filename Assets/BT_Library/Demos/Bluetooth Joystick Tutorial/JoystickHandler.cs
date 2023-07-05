using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using TechTweaking.Bluetooth;

public class JoystickHandler : MonoBehaviour {
	//main
	private  BluetoothDevice device;
	//text
	public TextMeshProUGUI statusText;
	public TextMeshProUGUI logsOnScreen; 
	//shoot & reload & joystick axes
	public float shoot;
	public float reload;
	public float joystickX;
	public float joystickY;

	void Awake () {
		
		BluetoothAdapter.enableBluetooth();//Force Enabling Bluetooth
		
		device = new BluetoothDevice();
		
		// We need to identefy the device either by its MAC Adress or Name (NOT BOTH! it will use only one of them to identefy your device).
		 
		device.Name = "HC-05";
		
		// 10 equals the char '\n' which is a "new Line" in Ascci representation, 
		// so the read() method will retun a packet that was ended by the byte 10. simply read() will read lines. 
		device.setEndByte (255);

		//The ManageConnection Coroutine will start when the device is ready for reading.
		device.ReadingCoroutine = ManageConnection;	
	}
	
	public void connect() {
        statusText.text = "Connecting...";
        device.connect();
	}
	
	public void disconnect() {
		device.close();
	}
	public BluetoothDevice GetDevice()
	{
		return device;
	}
	IEnumerator  ManageConnection (BluetoothDevice device)
	{
		statusText.text = "Status : Connected & Can read";
		while (device.IsConnected && device.IsReading) {
			
			//polll all available packets
			BtPackets packets = device.readAllPackets();
			
			if (packets != null) {
				
				/*
				 * parse packets, packets are ordered by indecies (0,1,2,3 ... N),
				 * where Nth packet is the latest packet and 0th is the oldest/first arrived packet.
				 * 
				 * Since this while loop is looping one time per frame, we only need the Nth(the latest potentiometer/joystick position in this frame).
				 * 
				 */
				int N = packets.Count - 1;
				
				//packets.Buffer contains all the needed packets plus a header of meta data (indecies and sizes) 
				//To get a packet we need the INDEX and SIZE of that packet.
				int indx = packets.get_packet_offset_index(N);
				int size = packets.get_packet_size(N);

                if (size == 8){
					// packets.Buffer[indx] equals lowByte(x1) and packets.Buffer[indx+1] equals highByte(x2)
					int val1 =  (packets.Buffer[indx+1] << 8) | packets.Buffer[indx];
					//Shift back 3 bits, because there was << 3 in Arduino
					val1 >>= 3;
					int val2 =  (packets.Buffer[indx+3] << 8) | packets.Buffer[indx+2];
					//Shift back 3 bits, because there was << 3 in Arduino
					val2 >>= 3;

                    // packets.Buffer[indx] equals lowByte(x1) and packets.Buffer[indx+1] equals highByte(x2)
                    int val3 = (packets.Buffer[indx + 5] << 8) | packets.Buffer[indx+4];
                    //Shift back 3 bits, because there was << 3 in Arduino
                    val3 >>= 3;
                    int val4 = (packets.Buffer[indx + 7] << 8) | packets.Buffer[indx + 6];
                    //Shift back 3 bits, because there was << 3 in Arduino
                    val4 >>= 3;

                    //#########Converting val1, val2 into something similar to Input.GetAxis (Which is from -1 to 1)#########
                    //since any val is from 0 to 1023
                    float Axis1 = ((float)val1/1023f)*2f - 1f;
					float Axis2 = ((float)val2/1023f)*2f - 1f;
                    float Axis3 = ((float)val3/1023f)*2f - 1f;
                    float Axis4 = ((float)val4/1023f)*2f - 1f;

                    logsOnScreen.text =  Axis1 + "\n" + Axis2 + "\n" + Axis3 + "\n" + Axis4;

					//#####joystick control#####
                    GetValues(Axis1,Axis2, Axis3, Axis4);

					 // Now Axis1 or Axis2  value will be in the range -1...1. Similar to Input.GetAxis
                    }	
				}
			
			yield return null;
		}
		statusText.text = "Status : Done Reading";
	}
	private void GetValues(float shooting, float reloading, float joyX, float joyY)
	{
		shoot = shooting;
		reload = reloading;
		joystickX= joyX;
		joystickY= joyY;
	}

}
