    let animals = [
        { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
        { name: "Carlo", species: "dog", class: { name: "vertebrata" } },
        { name: "Nemo", species: "fish", class: { name: "mamalia" } },
        { name: "Hamilton", species: "dog", class: { name: "mamalia" } },
        { name: "Dory", species: "fish", class: { name: "mamalia" } },
        { name: "Ursa", species: "cat", class: { name: "mamalia" } },
        { name: "Taro", species: "cat", class: { name: "vertebrata" } }
    ];

    console.log("1. species nya kucing saja");
    //for (let i = 0; i < animals.length; i++) {
    //    if (animals[i].species == "cat") {
    //        console.log(animals[i])
    //    }
    //}
let cats = [];
animals.filter(data => data.species == "cat" ? cats.push(data) : "");
console.log(cats);

    console.log("\n\n2. jika bukan mamalia (tulis Non-Mamalia)");
    for (let k in animals) {
        if (animals[k].class.name == "vertebrata") {
            animals[k].class.name = "Non-Mamalia";
        }
    }
console.log(animals);





