import React, { useEffect, useState } from "react";
import api from '../api/api.js';


function ManufacturerList(props) {
  const [manufacturers, setManufacturers] = useState();
  const [selectedId, setSelectedId] = useState(null);

  useEffect(() => {
    fill();
  }, []);

  const idSelected = (id) => {
    setSelectedId(id);
    props.idChanged(id);
  };

  return (
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
            <th>Year</th>
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
              <td>&nbsp;</td>
              <td>
                <button onClick={()=>idSelected(manufacturer.id)} type="button" className="btn btn-sm btn-primary mx-1">
                  <i className="bi bi-pencil-square"></i> Edit
                </button>
                <button type="button" className="btn btn-sm btn-danger mx-1">
                  <i className="bi bi-x-circle"></i> Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    )
  );
  async function fill() {
    const response = await api.get("manufacturerlist");
    const data = await response.json();
    setManufacturers(data);
  }
}

export default ManufacturerList;
