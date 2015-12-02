# EsploraPulse
An interface for measuring heart rate using a C# Windows Form application, an Arduino Esplora microcontroller, and a Pulse Sensor device.

## Images
![EsploraPulse Application](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/EsploraPulseApp.png "EsploraPulse Application") ![Arduino Esplora Setup](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/EsploraSetup.jpg "Arduino Esplora Setup")

## Parts
1. [Arduino Esplora Microcontroller](http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=arduino+esplora) (Discontinued; Price varies from ~$25 to $35)
2. [Pulse Sensor](https://www.sparkfun.com/products/11574) ($24.95)
3. [Visual Studio Community 2015](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx) (Free)
4. [Female-to-Female Jumper Wire (x3)](http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=female+to+female+jumper+wire&rh=i%3Aaps%2Ck%3Afemale+to+female+jumper+wire) (Amazon sells 40pc sets for ~$4)
5. [Hot Glue Gun](http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=hot+glue+gun) (~$5 to ~$10)
6. [Hot Glue Sticks](http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=hot+glue+sticks&rh=i%3Aaps%2Ck%3Ahot+glue+sticks) (Starting at ~$5)
7. [Painter's Tape](http://www.amazon.com/s/ref=nb_sb_noss_1?url=search-alias%3Daps&field-keywords=painter%27s+tape&rh=i%3Aaps%2Ck%3Apainter%27s+tape) (Starting at ~$4)

## Procedure

### Prepare the Pulse Sensor
1. Follow the steps in the [Pulse Sensor Getting Started](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/PulseSensorGettingStarted.pdf) guide starting at the "Preparing the Pulse Sensor" section. This file will guide you through protecting the Pulse Sensor from moisture/oil and yourself from exposed electrical components on the Pulse Sensor.
2. You can use either the velcro finger strap or the earlobe clip setup for your Pulse Sensor.

<sub>This Getting Started guide is derived from the Getting Started Guide located on this [page](https://www.sparkfun.com/products/11574) under the documents section. I did not create the Guide. All rights belong to the original authors.</sub>

### Connect the Pulse Sensor to the Arduino Esplora
1. Now we have to connect the Pulse Sensor to the Arduino Esplora. The Pulse Sensor is an analog sensor and, as such, requires an analog port to function. This is a problem because the Arduino Esplora doesn't have any easily accessible analog ports. It does, however, have two TinkerKit connectors which just so happen to be analog ports. These connectors are located on the top-right of the Arduino Esplora board (they are the white, three-male connectors). ![Esplora Pinout Diagram](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/ArduinoHeaders.PNG "Esplora Pinout")
2. Now that we know where to connect the sensor, we need to take a look at the sensor's headers. Notice that these headers are also male. This is where our Female-to-Female jumper wires will come in handy.![Pulse Sensor Headers](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/PulseSensorHeaders.PNG "Sensor Headers")
  1. First, connect the black (GND) wire of the Pulse Sensor to the leftmost TinkerKit connector's (INA) rightmost pin (GND on the diagram) using a Female-to-Female wire.
  2. Second, connect the purple (Signal) wire of the Pulse Sensor to the leftmost TinkerKit connector's (INA) middle pin (23 X8 on the diagram) using a different Female-to-Female wire.
  3. Finally, connect the red (+3V/+5V) wire of the Pulse Sensor to the leftmost TinkerKit connector's (INA) leftmost pin (5V on the diagram) using a third and final Female-to-Female wire.
3. When you have finished connecting all three wires, you are finished connecting the Pulse Sensor and the Arduino Esplora. Your setup should look something like this: ![Connected Esplora and Sensor](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/Connected.jpg "Connected Esplora and Sensor")

### Writing Arduino Code
Now we have to write our Arduino code.
1. Download the [Arduino IDE](https://www.arduino.cc/en/Main/Software).
2. Plug your Arduino Esplora into one of your computer's USB ports.
3. Open the Arduino IDE and tell the IDE where it can find your Esplora.
  1. Go to Tools on the Menu bar and set Board to Board: "Arduino Esplora".
  2. Go to Tools on the Menu bar and set Port to the port where you connected the Esplora (i.e. COM4). It should be the only available choice. **Remember this port name for later!**
4. Now you can enter this code: ![Arduino Code](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/ArduinoCode.PNG "Arduino Code")
  * The #include <Esplora.h> is necessary for the compiler to recognize the Esplora board.
  * The CH_TINKERKIT_INA constant is the port on the Arduino Esplora where the Pulse Sensor is connected.
  * The READ_RATE constant how often, in milliseconds, the Esplora will read from the Pulse Sensor and print the reading to the computer via the Serial port.
  * The readChannel function is necessary for the Esplora to read the TinkerKit connector as an analog port. DO NOT CHANGE THIS CODE!
  * The setup function runs before the main code. It sets up the Serial port connection with a baud rate of 9600. (9600 bits per second)
  * The loop function reads the sensor and prints it to the Serial port every so often (determined by the READ_RATE).
5. Press the Arrow at the top of the IDE to upload the code to your Arduino Esplora. This is the only time you should have to do this.

### Writing the C# Code
