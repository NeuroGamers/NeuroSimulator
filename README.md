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
