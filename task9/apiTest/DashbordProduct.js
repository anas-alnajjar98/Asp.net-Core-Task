async function getAllCategory() {
    let url = "https://localhost:7216/api/Products";
    let request = await fetch(url);
    let data = await request.json();
    let tableBody = document.getElementById("ProductTableBody");

    tableBody.innerHTML = ""; 
    data.forEach((Product) => {
        let row = `
            <tr>
                <td>${Product.productId}</td>
                <td>${Product.productName}</td>
                <td>${Product.description}</td>
                <td><img src="${Product.productImage}" alt="Image not found" style="width: 50px; height: 50px;"></td>
                <td>${Product.price}</td>
                <td>${Product.categoryId}</td>
                <td>
                    <button class="btn btn-warning btn-sm" onclick="editCategory(${Product.productId})">Edit</button>
                </td>
            </tr>`;
        tableBody.innerHTML += row;
    });

    console.log(data);
}

function editCategory(id) {
    localStorage.setItem("ProductID",id)
    const newPageUrl = '/editProduct.html';
    window.location.href=newPageUrl
    alert(`Edit Product with ID: ${id}`);
}

function createNewCategory() {
    const newPageUrl = '/AddProduct.html';
    window.location.href=newPageUrl
    alert("Create a new category");
   
}

getAllCategory();
