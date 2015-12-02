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
