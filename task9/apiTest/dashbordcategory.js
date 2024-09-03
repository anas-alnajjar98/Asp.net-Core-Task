async function getAllCategory() {
    let url = "https://localhost:7216/api/Categories";
    let request = await fetch(url);
    let data = await request.json();
    let tableBody = document.getElementById("categoryTableBody");

    tableBody.innerHTML = ""; 
    data.forEach((category) => {
        let row = `
            <tr>
                <td>${category.categoryId}</td>
                <td><img src="${category.categoryImage}" alt="Image not found" style="width: 50px; height: 50px;"></td>
                <td>${category.categoryName}</td>
                <td>
                    <button class="btn btn-warning btn-sm" onclick="editCategory(${category.categoryId})">Edit</button>
                </td>
            </tr>`;
        tableBody.innerHTML += row;
    });

    console.log(data);
}

function editCategory(id) {
    localStorage.setItem("CategoryID",id)
    const newPageUrl = '/editCategory.html';
    window.location.href=newPageUrl
    alert(`Edit category with ID: ${id}`);
}

function createNewCategory() {
    const newPageUrl = '/Addcategory.html';
    window.location.href=newPageUrl
    alert("Create a new category");
   
}

getAllCategory();
