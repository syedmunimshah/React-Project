import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import "bootstrap/dist/css/bootstrap.min.css";

const Crud = () => {
  const empdata = [
    {
      id: 1,
      name: "Ali",
      age: 29,
      isActive: 1,
    },
    {
      id: 2,
      name: "Syed",
      age: 30,
      isActive: 0,
    },
    {
      id: 3,
      name: "Shah",
      age: 31,
      isActive: 1,
    },
    {
      id: 4,
      name: "Munim",
      age: 32,
      isActive: 0,
    },
    {
      id: 5,
      name: "Abdul",
      age: 33,
      isActive: 1,
    },
  ];
  const [data, setData] = useState([]);
  useEffect(() => {
    setData(empdata);
  }, []);

  return (
    <>
      <Table striped bordered hover>
        <thead>
          <tr>
          <th>#</th>
            <th>ID</th>
            <th>Name</th>
            <th>Age</th>
            <th>IsActive</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0
            ? data.map((item, index) => {
                return (
                  <tr key={index}>
                    <td>{index +1}</td>
                    <td>{item.id}</td>
                    <td>{item.name}</td>
                    <td>{item.age}</td>
                    <td>{item.isActive}</td>
                  </tr>
                );
              })
            : "loading......"}
        </tbody>
      </Table>
    </>
  );
};

export default Crud;
