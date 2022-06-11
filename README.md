# NeuroSimulator
This repository allows for the connection of a cart game on Unity with EEG signals acquired through a headset compatible with the OpenVIBE Acquisition Server. A basic architecture of the working of the system is shown below.
<p align="center">
</br><img src="https://user-images.githubusercontent.com/48885205/170839794-f8b0c150-50c5-4c90-9810-3596b23bc666.png" width="500" height="500">
</p>

## **Requirements**
The requirements for using this project are:
- Python 3.9
- Tensorflow 2.8
- Unity 2022
- OpenVIBE 3.2.0

## **Specifications for the Incoming Signal**
The EEG signal this system is trained for is of:
- Sampling Frequency of 160Hz
- 16 electrode channels

## **Training of Model**
You can use the pre-trained model for this, or you can train your own with either your data or the one we used which is publically available at: https://drive.google.com/drive/folders/11tCrbFUudiq6_ADMQRNwThn3n-eYqREv?usp=sharing

## **How to use**
Follow the following steps
1. Add "Streaming_and_Classification.py", "Interface_With_Agent.cs" and "Pre-Trained Model" (or your own saved model if you have trained it yourself) in the scripts entry of your agent (prefrably a carting agent).
2. Turn on the OpenVIBE Aquisition Server and adjust the parameters to your choosing and select the used driver after connecting the EEG headset.
3. Run "LSL_Exporting.xml"
4. Run the game
5. Run "Streaming_And_Classification.py"
6. Enjoy!

## **Results**
We ran this on a wheelchair simulator game, available at https://github.com/zeerakt/EEGCart
We had the following results on the Physics Simulation.
- Moving Forward
<p align="center">
</br><img src="https://user-images.githubusercontent.com/48885205/173187858-3e758f7c-f5d4-4071-aec6-81256fb6ca13.png" width="400" height="200">
</p>
- Turning Left
<p align="center">
</br><img src="https://user-images.githubusercontent.com/48885205/173187875-dadd2b29-a49a-4f21-9036-a3c45957846d.png" width="400" height="200">
</p>
- Turning Right
<p align="center">
</br><img src="https://user-images.githubusercontent.com/48885205/173187901-297caf79-98d9-4efb-b610-f0b81c2374da.png" width="400" height="200">
</p>

## **Credits**
- To https://github.com/hauke-d for the CNN Model
- To https://github.com/CanYouCatchMe01 for the Socket Communication
- To https://github.com/sccn for the Lab Streaming Layer
