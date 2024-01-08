import React, { useEffect, useState } from "react";
import api from "../api/api.js";

function ConsoleList(props) {
const [consoles, setConsoles] = useState();
const [selectedId, setSelectedId] = useState(null);

  useEffect(() => {
    fill();
  }, []);

  const idSelected = (id) => {
    setSelectedId(id);
    props.idChanged(id);
  };

  return (
    consoles === undefined ? (
      <p>
        <em>Loading... </em>
      </p>
    ) : (
      <table
        className="table table-striped"
        aria-labelledby="tableConsole"
      >
        <thead>
          <tr>
          <th>Manufacturer</th>
          <th>Name</th>
            <th>Description</th>
            <th>Year</th>
            <th>Still active?</th>
            <th>#</th>
          </tr>
        </thead>
        <tbody>
          {consoles.map((console) => (
            <tr key={console.id}>
              <td>{console.manufacturerName}</td>
              <td>{console.name}</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
              <td>
                <button onClick={()=>idSelected(console.id)} type="button" className="btn btn-sm btn-primary mx-1">
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
    const response = await api.get("consolelist");
    const data = await response.json();
    setConsoles(data);
  }
}

export default ConsoleList;
