# A helping hand
***
The other day I set a bit of a challange; To complete the `SimpleSerializer` class with Save/Load Vector functions.
I gave you just two hints to try do this. These hints were:
1. You may need to use a string array.
2. You may want to use `File.WriteAllLines()` instead of `File.WriteAllText()`.

## Establishing the problem 
Before solving any problem it is essential that you first understand it (This is true of every type of problem in life, Maths, Science, Logic or other).
The problem we wish to solve is the following:
> Be able to convert a Vector to a text format before saving it. Then be able to convert this text back to a vector

We now need to start thinking 'like a programmer' and analyse the problem to break it into simple steps. Lets examine our `SaveInt()` function in order to
to see what simple steps were used in it.
```C#
public static void SaveInt(string key, int value) {
	string PATH = Application.StreamingAssetPath + "/" + key + ".txt";
	File.WriteAllText(PATH, value.ToString();
}
```
We can break this down to the folowing:
1. Take a save key and value (int) from the caller
2. Use the key to create a save-file path
3. Convert value (int) to string data using `int.ToString()`
4. Write the data using `WriteAllText()`
With that in mind lets think about what the steps would look like for `SaveVector()`
1. Take a save key and value (Vector2) from the caller
2. Use the key to create a save-file path
3. Convert value (Vector2) to string data using ???
4. Write the data using `WriteAllText()`
Note step three, the issue arises when trying to convert Vector data to string data. How can this be done. Well if you think about it, vectors simply conatin two
float values `Vector.x` and `Vector.y`. Using these values we can get two string from the vector, one for each coordinate. But how can we differentiate the between these two values when reading from the file.
The first method might be to write the first string and the second string seperated by a special character eg; `5.666, 7.888` (where x = 5.666 and y = 7.888). We could later use `string.Split()` or `egex.Split()` to break these two values up, however there is also a much simpler method.
Here is where our hints come into play. `WriteAllLines()` takes a string array instead of just a single string. It then writes each string in the array to a new line in the file. `ReadAllLines()` returns a string array where each new line in the file is added as a new element to the end of the array.
This means we can simply write each vector value to a new line and then read them back the same way.
```C#
public static void SaveVector(string key, Vector2 value) {
	string PATH = Application.StreamingAssetPath + "/" + key + ".txt";
	
	string[] data = new string[2]; //Initialises array with two values (for x and y)
	data[0] = value.x.ToString();  //Sets the first value to vector's x element
	data[1] = value.y.ToString();  //Sets the second element to vector's y element
	
	File.WriteAllLines(PATH, data);
}
```
```C#
public static Vector2 LoadVector(string key) {
	string PATH = Application.StreamingAssetPath + "/" + key + ".txt";
	
	string[] data = File.ReadAllLines(PATH);
	float x = float.Parse(data[0]); //converts first element to float and sets it to the x value
	float y = float.Parse(data[1]); //converts second element to float and sets it to the y value
	
	return new Vector2(x, y); //returns the vector, NOTE: the function must have Vector2 return type
}
```
