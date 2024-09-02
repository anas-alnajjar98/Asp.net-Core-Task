const n = localStorage.getItem("CategoryID");
debugger
var url = `https://localhost:7216/api/Categories/${n}`;
var form = document.getElementById("form");
async function fun2() {
   
    var formData = new FormData(form);
    event.preventDefault();
    let response = await fetch(url, {
        method: 'PUT',
        body: formData,
    });
    var data = response;
     window.location.href = "dashbordcategory.html"
    alert("your category has successfully edited");
}