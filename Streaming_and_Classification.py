from pylsl import StreamInlet, resolve_stream
import pyedflib
import numpy as np
from scipy import signal
from random import randrange
from tensorflow import keras
import socket
import time
host, port = "127.0.0.1", 25001          
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))
model = keras.models.load_model('Pre-Trained Model')

#Loading signal data from .edf file

def load_signal_from_edffile(path):
  f= pyedflib.EdfReader(path)
  n = f.signals_in_file
  signal_labels = f.getSignalLabels()
  sigbufs = np.zeros((n, f.getNSamples()[0]))
  for i in np.arange(n):
          sigbufs[i, :] = f.readSignal(i)
  annotations = f.read_annotation()
  f._close()
  return sigbufs.transpose(), annotations

#Signal Processing for manipulating data 

def preprocessing(x):
  x = np.array(x)
  x = x.T
  b, a = signal.butter(5, 5/80, 'high')
  x = signal.filtfilt(b,a,x)
  b, a = signal.butter(5, 45/80, 'low')
  x = signal.filtfilt(b,a,x)
  x = x.reshape((1,240,16))
  return x

#Appropriate vectors for sending to application (Unity3D)

def vector_formation(g):
  if g == 0:
    v = [1,0,0]
  elif g == 1:
    v = [0,1,0]
  else:
    v = [0,0,1]
  return v

#x,y = load_signal_from_edffile('S029R07.edf')


  #Initiating Socket Communication


while True:

  streams = resolve_stream('type', 'EEG')
  chunk = []
  inlet = StreamInlet(streams[0])
  while len(chunk) < 240:
    chunk_now, timestamps = inlet.pull_sample()
    chunk.append(chunk_now)
    print(len(chunk))

  data2 = preprocessing(chunk)
  p = model.predict(data2)
  vector = vector_formation(np.argmax(p))
  print(vector)

  

  
  posString = ','.join(map(str, vector))
  print(posString)

    #Sending our vector, in our 3D Basis

  sock.sendall(posString.encode("UTF-8")) 
  time.sleep(0.2) 
