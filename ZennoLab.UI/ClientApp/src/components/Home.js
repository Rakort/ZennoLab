import React, { Component } from 'react';
import axios from "axios";

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        var date = new Date();
        var formatedDate = `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`

        this.state = {
            name: "",
            createdAt: formatedDate,
            isCyrillic: false,
            isLatin: false,
            isNumbers: false,
            isSpecialChar: false,
            caseSensitivity: false,
            locationAnswer: "None",
            archive: null,
            rows: [],
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : (target.type === 'file' ? target.files[0] : target.value);
        const name = target.name;
        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        event.preventDefault();

        try {
            let formData = new FormData();
            formData.append("name", this.state.name);
            formData.append("createdAt", this.state.createdAt);
            formData.append("isCyrillic", this.state.isCyrillic);
            formData.append("isLatin", this.state.isLatin);
            formData.append("isNumbers", this.state.isNumbers);
            formData.append("isSpecialChar", this.state.isSpecialChar);
            formData.append("caseSensitivity", this.state.caseSensitivity);
            formData.append("locationAnswer", this.state.locationAnswer);
            formData.append("archive", this.state.archive, this.state.archive.name);

            axios
                .post("https://localhost:44380/DataSet/upload", formData, {
                    headers: { 'Content-Type': 'multipart/form-data' }
                })
                .then(res => {
                    var rows = this.state.rows
                    rows.push({ name: this.state.name, createdAt: this.state.createdAt })
                    this.setState({ rows: rows })
                    this.setState({ errors: [] })
                })
                .catch(
                    error => {
                        console.log(error.response);
                        this.setState({ errors: error.response.data.errors });
                    });
        } catch (errors) {
            console.log(errors.response);
        }
    }

    render() {
        return (
            <div>
                <h1>Upload Dataset</h1>
                <p>?????????? ???? ???????????? ?????????????????? ???????? ?????????????????????? ???????????????? ?? ???????????????????? ?? ?????????????? ?????? ???????????????? ?????????????????? ?????????? ?? ???????????????? ?????????? ?????????????? ??????????????????????????.</p>

                <form onSubmit={this.handleSubmit}>
                    <div>
                        <label htmlFor="name">??????: </label>
                        <input type="text" name="name" onChange={this.handleInputChange} />
                        {this.state.errors?.Name?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="createdAt">???????? ????????????????: </label>
                        <input type="date" name="createdAt" onChange={this.handleInputChange} />
                        {this.state.errors?.CreatedAt?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="isCyrillic">???????????????? ??????????????????: </label>
                        <input type="checkbox" name="isCyrillic" onChange={this.handleInputChange} />
                        {this.state.errors?.IsCyrillic?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="isLatin">???????????????? ????????????????: </label>
                        <input type="checkbox" name="isLatin" onChange={this.handleInputChange} />
                        {this.state.errors?.IsLatin?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="isNumbers">???????????????? ??????????: </label>
                        <input type="checkbox" name="isNumbers" onChange={this.handleInputChange} />
                        {this.state.errors?.IsNumbers?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="isSpecialChar">???????????????? ?????????????????????? ??????????????: </label>
                        <input type="checkbox" name="isSpecialChar" onChange={this.handleInputChange} />
                        {this.state.errors?.IsSpecialChar?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="caseSensitivity">???????????????????????????????? ?? ????????????????: </label>
                        <input type="checkbox" name="caseSensitivity" onChange={this.handleInputChange} />
                        {this.state.errors?.CaseSensitivity?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="locationAnswer">???????????????? ???????????????????????? ?????????????? ???? ????????????????: </label>
                        <select name="locationAnswer" value={this.state.value} onChange={this.handleInputChange} >
                            <option value="None">??????????????????????</option>
                            <option value="FileNames"> ?? ???????????? ????????????</option>
                            <option value="SeparateFile"> ?? ?????????????????? ??????????</option>
                        </select>
                        {this.state.errors?.LocationAnswer?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>
                    <div>
                        <label htmlFor="archive">?????????? ????????????????: </label>
                        <input type="file" name="archive" onChange={this.handleInputChange} />
                        {this.state.errors?.Archive?.map((error) => <p className="p isa_error" key={error}>{error}</p>)}
                    </div>

                    <input type="submit" value="??????????????????" disabled={!this.state.archive}/>
                </form>

                <h3>?????????????????????? ????????????????</h3>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>??????</th>
                            <th>???????? ????????????????</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.rows.map((row, i) =>
                            <tr key={i}>
                                <td>{row.name}</td>
                                <td>{row.createdAt}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}
