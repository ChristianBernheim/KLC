
//importera data från annan .js fil (istället för api)
import data from "/js/data.js";

//hämta html element
const canvas = document.querySelector("#advanced-chart");

//sätt höjd bredd på canvas
canvas.width = 500;
canvas.height = 400;

//hämta canvas kontext
const ctx = canvas.getContext("2d");

//funktion som returnerar array med "mätning1, mätning2" etc.
const counts = () => {
  let countArray = [];
  for (let index = 1; index < data.length + 1; index++) {
    countArray.push("Mätning " + index);
  }
  return countArray;
};

//värden som kommer återanvändas
let mBorderwidth = 8;
let mHoverBorderwidth = 30;
let mTension = 0.2;

//skapa chart
let mChart = new Chart(ctx, {
  type: "line",
  data: {
    //använder counts funktionen ovan
    //kommer bli ytterligare en data.map sen
    labels: counts(),
    datasets: [
      {
        label: "Andningsfrekvens",
            //hämtar alla värden ur data arrayen, för key "andningsfrekvens"
            data: data.map((value) => Math.abs(value.andningsfrekvens)),
        //Inställningar för chart, ex. hur mycket bezier curve man vill ha
        borderColor: "#488f31",
        backgroundColor: "#488f31",
        borderWidth: mBorderwidth,
        tension: mTension,
        pointHoverRadius: mHoverBorderwidth,
      },
      {
          label: "Syremättnad",
          data: data.map((value) => {
              if (value.syremättnad >= 7) {
                  value.syremättnad = value.syremättnad - 10;
              }
              return Math.abs(value.syremättnad);
          }),
        borderColor: "#8aac49",
        backgroundColor: "#8aac49",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
      {
        label: "Tillförd syrgas",
          data: data.map((value) => Math.abs(value.syrgas)),
        borderColor: "#c6c96a",
        backgroundColor: "#c6c96a",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
      {
        label: "Systoliskt blodtryck",
          data: data.map((value) => Math.abs(value.systolisktblodtryck)),
        borderColor: "#ffe792",
        backgroundColor: "#ffe792",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
      {
        label: "Pulsfrekvens",
          data: data.map((value) => Math.abs(value.pulsfrekvens)),
        borderColor: "#f8b267",
        backgroundColor: "#f8b267",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
      {
        label: "Medvetandegrad",
          data: data.map((value) => Math.abs(value.medvetandegrad)),
        borderColor: "#eb7a52",
        backgroundColor: "#eb7a52",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
      {
        label: "Temperatur",
          data: data.map((value) => Math.abs(value.temperatur)),
        borderColor: "#de425b",
        backgroundColor: "#de425b",
        borderWidth: mBorderwidth,
          tension: mTension,
          pointHoverRadius: mHoverBorderwidth,
      },
    ],
  },
  options: {
    scales: {
      y: {
        min: 0, // Minsta värde
        max: 4, // Högsta värde

        ticks: {
            stepSize: 1, // steg mellan värden
            // Sätt de värden vi vill ha på Y-axis
            callback: function (value, index, values) {
                var labels = [
                    "0",
                    "1",
                    "2",
                    "3",
                    "",
                ];

                return labels[index];
          },
        },
      },
    },
  },
});

export default mChart;
