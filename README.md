# EsploraPulse
An interface for measuring and visualizing your heart rate using a C# Windows Form application, an Arduino Esplora microcontroller, and a Pulse Sensor device.

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
  * Go to Tools on the Menu bar and set Board to Board: "Arduino Esplora".
  * Go to Tools on the Menu bar and set Port to the port where you connected the Esplora (i.e. COM4). It should be the only available choice. **Remember this port name for later!** ![Arduino Code](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/ArduinoCode.PNG "Arduino Code")
4. Enter the above code. 
  * The #include is necessary for the compiler to recognize the Esplora board.
  * The CH_TINKERKIT_INA constant is the port on the Arduino Esplora where the Pulse Sensor is connected.
  * The READ_RATE constant how often, in milliseconds, the Esplora will read from the Pulse Sensor and print the reading to the computer via the Serial port.
  * The readChannel function is necessary for the Esplora to read the TinkerKit connector as an analog port. DO NOT CHANGE THIS CODE!
  * The setup function runs before the main code. It sets up the Serial port connection with a baud rate of 9600. (9600 bits per second)
  * The loop function reads the sensor and prints it to the Serial port every so often (determined by the READ_RATE).
5. Press the Arrow at the top of the IDE to upload the code to your Arduino Esplora. This is the only time you should have to do this.

### Writing the C# Code
It is now time to write the rest of our code in C# using Visual Studio 2015.

1. Create a new C# Web Form Application named EsploraPulse. Add folders and classes (Right-click the Project and click Add...) to the project until your directory structure looks like this: ![Project Structure](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/DirectoryStructure.png "Project Structure")
2. Create the [PulseData](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/Model/PulseData.cs) class.
  * This class holds the data necessary for calculating a heart rate.
3. Create the [PulseCalculator](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/Model/PulseCalculator.cs) class.
  * The function of most importance here is CalculatePulse. This is where the raw sensor data is converted into more meaningful information.
4. Create the [EsploraPulseController](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/Controller/EsploraPulseController.cs) class.
  * This class is used by the View classes to communicate with the Model classes. It contains a PulseCalculator object which it instructs to calculate the pulse, whenever such a calculation is necessary.
5. Create the [ErrorHandler](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/Static/ErrorHandler.cs) class.
  * This class has a very simple job. It is used to display any exceptions to the user in a more friendly manner than crashing the Application in a wall of red text.
6. Create the EsploraPulseForm class.
  * This class consists of a Designer section and the actual class code.
  * The Designer is where you will can drag components from the Toolbox and customize the appearance of the form. The form has a menu bar, two labels for BPM, three buttons (Start, Stop, EmailBPM), a Chart, and a Serial Port component (named EsploraSerial). **It is important to make sure that the properties are set as I have them in the source code. A lot of this Designer class is not explicitly coded and is just implemented by the GUI by default.** ![EsploraPulse Form Designer](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/EsploraPulseForm.png "Designer Form")
  * The [EsploraPulseForm](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/View/EsploraPulseForm.cs) class contains the necessary setup code for the Serial Port and Chart. It also contains code to tell the controller to calculate the pulse and display the EmailForm.
7. Create the EmailForm class.
  * This class consists of a Designer section and the actual class code.
  * The Designer is where you will can drag components from the Toolbox and customize the appearance of the form. The form has three labels, three text boxes, and two buttons (Send and Cancel). **It is important to make sure that the properties are set as I have them in the source code. A lot of this Designer class is not explicitly coded and is just implemented by the GUI by default.** ![EmailForm](https://github.com/jwalke24/EsploraPulse/blob/master/Resources/Images/EmailForm.png)
  * The [EmailForm](https://github.com/jwalke24/EsploraPulse/blob/master/EsploraPulse/View/EmailForm.cs) class contains the setup code for the SMTP email client.
8. After implementing these classes, you should have successfully finished the C# coding portion of the Project.
  * Make sure you read through the classes I created to get a better understanding of how everything is set up before you try to create the classes from scratch.

### Endgame
At this point, the project should be ready to run. Just plug in your Arduino Esplora, ensure you uploaded the Arduino code, and click the Start button in Visual Studio.
  1. Place your finger in the cuff (or clip the sensor to your ear).
  1. Press Start to begin reading data and drawing the chart.
  2. Press Stop to pause the chart and readings at a certain value.
  3. Press EmailBPM to email the current BPM value to anyone of your choice (currently only works to/from gmail addresses).

### Other Notes
* Don't press too hard on the Pulse Sensor, this will cause your BPM to read as a large (and incorrect) value.
