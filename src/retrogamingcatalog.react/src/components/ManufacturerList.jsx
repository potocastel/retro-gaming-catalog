import React, { useEffect, useState } from "react";

function ManufacturerList() {
  const [manufacturers, setManufacturers] = useState();

  useEffect(() => {
    fill();
  }, []);

  const contents =
    manufacturers === undefined ? (
      <p>
        <em>Loading... </em>
      </p>
    ) : (
      <table
        className="table table-striped"
        aria-labelledby="tableManufacturers"
      >
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Still active?</th>
            <th>#</th>
          </tr>
        </thead>
        <tbody>
          {manufacturers.map((manufacturer) => (
            <tr key={manufacturer.id}>
              <td>{manufacturer.name}</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
              <td> 
                  <button type="button" class="btn btn-sm btn-primary mx-1">
                    <i className="bi bi-pencil-square"></i> Edit
                  </button>
                  <button type="button" class="btn btn-sm btn-danger mx-1">
                  <i className="bi bi-x-circle"></i> Delete
                  </button> 
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );

  return (
    <div>
      <div className="d-flex flex-row gap-3">
        <h1 id="tableManufacturers">Manufacturer list</h1> 
          <button type="button" class="btn btn-success align-self-center">
            <i className="bi bi-plus-circle"></i> Add
          </button>  
      </div>
      {contents}
    </div>
  );
  async function fill() {
    const response = await fetch("manufacturerlist");
    const data = await response.json();
    setManufacturers(data);
  }
}

export default ManufacturerList;
