import React, { useEffect, useRef, useState } from "react";
import Table from "react-bootstrap/Table";
import "bootstrap/dist/css/bootstrap.min.css";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const Crud = () => {
  const [data, setData] = useState([]);
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [name, setName] = useState("");
  const [age, setAge] = useState("");

  const isActive = useRef(false);
  const [editIsActive, setEditIsActive] = useState(false);

  const [editId, setEditId] = useState("");
  const [editName, setEditName] = useState("");
  const [editAge, setEditAge] = useState("");

  const handleEdit = (id) => {
    handleShow();
    axios
      .get(`https://localhost:7271/api/Employees/${id}`)
      .then((result) => {
        setEditName(result.data.name);
        setEditAge(result.data.age);
        setEditIsActive(result.data.isActive);
        setEditId(id);
      })
      .catch((error) => {
        toast.error(error.message);
      });
  };

  const handleDelete = (id) => {
    if (window.confirm("Are you sure you want to delete this employee?")) {
      axios
        .delete(`https://localhost:7271/api/Employees/${id}`)
        .then((result) => {
          if (result.status === 200) {
            toast.success("Employee has been deleted");
            getData();
          }
        })
        .catch((error) => {
          toast.error(error.message);
        });
    }
  };

  const handleUpdate = () => {
    const url = `https://localhost:7271/api/Employees/${editId}`;
    const data = {
      Id: editId,
      Name: editName,
      Age: editAge,
      IsActive: editIsActive,
    };
    axios
      .put(url, data)
      .then((result) => {
        handleClose();
        getData();
        clear();
        toast.success("Employee has been updated");
      })
      .catch((error) => {
        toast.error(error.message);
      });
  };

  const getData = () => {
    axios
      .get("https://localhost:7271/api/Employees")
      .then((result) => {
        console.log(result.data); // Debugging: Log data to console
        setData(result.data);
      })
      .catch((error) => {
        toast.error(error.message);
      });
  };

  useEffect(() => {
    getData();
  }, []);

  const handleSave = () => {
    const url = "https://localhost:7271/api/Employees";
    const data = {
      Name: name,
      Age: age,
      IsActive: isActive.current,
    };
    axios
      .post(url, data)
      .then((result) => {
        getData();
        clear();
        toast.success("Employee has been added");
      })
      .catch((error) => {
        toast.error(error.message);
      });
  };

  const clear = () => {
    setName("");
    setAge("");
    isActive.current = false;
    setEditName("");
    setEditAge("");
    setEditIsActive(false);
    setEditId("");
  };

  const handleActiveChange = (e) => {
    isActive.current = e.target.checked;
  };

  const handleEditActiveChange = (e) => {
    setEditIsActive(e.target.checked);
  };

  return (
    <>
      <ToastContainer />
      <Container>
        <Row className="d-flex align-items-center pt-5 pb-5">
          <Col>
            <input
              type="text"
              className="form-control"
              placeholder="Enter Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </Col>
          <Col>
            <input
              type="text"
              className="form-control"
              placeholder="Enter Age"
              value={age}
              onChange={(e) => setAge(e.target.value)}
            />
          </Col>
          <Col>
            <input type="checkbox" onChange={handleActiveChange} />
            <label>IsActive</label>
          </Col>
          <Col>
            <Button variant="primary" onClick={handleSave}>
              Submit
            </Button>
          </Col>
        </Row>
      </Container>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modify / Update Employee</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row className="d-flex align-items-center">
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Name"
                value={editName}
                onChange={(e) => setEditName(e.target.value)}
              />
            </Col>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Age"
                value={editAge}
                onChange={(e) => setEditAge(e.target.value)}
              />
            </Col>
            <Col>
              <input
                type="checkbox"
                checked={editIsActive}
                onChange={handleEditActiveChange}
              />
              <label>IsActive</label>
            </Col>
          </Row>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>

      <Table striped bordered hover className="text-center">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Age</th>
            <th>IsActive</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0 ? (
            data.map((item, index) => (
              <tr key={index}>
                <td>{index + 1}</td>
                <td>{item.name}</td>
                <td>{item.age}</td>
                <td>{item.isActive ? "1" : "0"}</td>
                <td>
                  <button
                    type="button"
                    className="btn btn-primary"
                    onClick={() => handleEdit(item.id)}
                  >
                    Edit
                  </button>
                  &nbsp;
                  <button
                    type="button"
                    className="btn btn-danger"
                    onClick={() => handleDelete(item.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="5">No data available</td>
            </tr>
          )}
        </tbody>
      </Table>
    </>
  );
};

export default Crud;
