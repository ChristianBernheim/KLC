
//hämta html element
const canvas = document.querySelector("#simple-chart");
console.log("hello");

canvas.width = 500;
canvas.height = 400;

//hämta canvas kontext
const ctx = canvas.getContext("2d");

const ActionCalc = (sum, threeflag) => {
  if (sum >= 7) {
    return 4;
  } else if (sum==6 || sum==5) {
    return 3;
  } else if (threeflag == true) {
    return 2;
  } else if (sum <= 4 && sum >= 1) {
    return 1;
  } else {
    return 0;
  }
};

const NewsCalcToArray = (array) => {
  //store our results
  let results = [];

  array.forEach((element) => {
    //inner result
    let result = { sum: 0, action: 0, time:0};
    let threeFlag = false;

    //keys that we are interested in
    const keys = [
      "andningsfrekvens",
      "medvetandegrad",
      "pulsfrekvens",
      "syreMattnad",
      "tillfordSyrgas",
      "systolisktBlodtryck",
      "temperatur",
      ];

    //make a map of the keys
      element = keys.map((key) => element[key]);
    //foreach element in keys map
      element.forEach((value) => {
          
          if (value != null) {
              console.log(value);
              //if syrgas2
              if (value >= 7) {
                  value = value - 10;
                  console.log(value);
              }
              //add to sum
              result.sum += Math.abs(value);
              //if any value is three flip flag
              if (Math.abs(value) == 3) {
                  threeFlag = true;
              }
          }
        });
      result.action = ActionCalc(result.sum, threeFlag);
      result.time = element.tidForMatning;
        results.push(result);
  });
  return results;
};

//importera data från annan .js fil (istället för api)
const mChart = async (id) => {
const response = await fetch("https://informatik13.ei.hv.se/KLCAPI/api/MatningNews2/GetAllFromPatient/" + id);
const srcdata = await response.json();
console.log(srcdata);
let mdata = NewsCalcToArray(srcdata);

//värden som kommer återanvändas
let mBorderwidth = 8;
let mHoverBorderwidth = 30;
let mTension = 0.2;

//skapa chart
  new Chart(ctx, {
  type: "line",
  data: {
      //använder counts funktionen ovan
      labels: mdata.map((value) => value.time),
    datasets: [
      {
        label: "NEWS",
        //hämtar alla värden ur data arrayen
        data: mdata.map((value) => value.action),
        borderColor: "#488f31",
        backgroundColor: "#488f31",
        borderWidth: mBorderwidth,
        //hur mycket bezier curve man vill ha
        tension: mTension,
        hoverBackgroundColor: "#ffffff",
        pointHoverRadius: mHoverBorderwidth,
      },
    ],
  },
  options: {
    scales: {
      y: {
        min: 0, // Minsta värde
        max: 5, // Högsta värde
        ticks: {
          stepSize: 1, // steg mellan värden
          // Sätt de värden vi vill ha på Y-axis
          callback: function (value, index, values) {
            var labels = [
              "0",
              "Totalt 1-4",
              "3 poäng i ett fält",
              "Totalt 5-6",
              "Totalt ≥7",
            ];

            return labels[index];
          },
        },
      },
    },
  },
});
}



export default mChart;
