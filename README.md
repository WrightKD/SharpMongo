# Sharp Mongo

Pymongo syntax usage for c# MongoDB access and usage. 

## Installation

Clone from Azure

```bash
git clone -b Regression repo
```

## Usage

```c#
using SharpMongo

Database Source = Data.connect("mongo://localhost:2704"); 

Collection Collection = new Collection();
var newCollection = Collection["student-data"];

Document Document = new Document();
string query = "{age : 15}";

var document = Document[query];

Dictionary<string, string> Students = new Dictionary<string, string>(5);
Students.Add("Kenneth", "Wright");
Students.Add("James", "McAuther");

Source.add(Students)
```

## Contributing
Pull requests are welcome.Please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
