const express = require('express');
const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const bodyParser = require('body-parser');
const cors = require('cors');

const User = require('./models/User');
const Position = require('./models/Position');

const app = express();
app.use(cors());
app.use(bodyParser.json());

// Kết nối đến MongoDB
mongoose.connect('mongodb://localhost:27017/game_db', { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('Kết nối đến MongoDB thành công!'))
    .catch(err => console.error('Kết nối đến MongoDB thất bại:', err));

// API đăng ký
app.post('/register', async (req, res) => {
    const { username, password } = req.body;
    const hashedPassword = bcrypt.hashSync(password, 8);

    try {
        const user = new User({ username, password: hashedPassword });
        await user.save();
        res.status(200).send('Đăng ký thành công!');
    } catch (err) {
        res.status(500).send('Đăng ký không thành công');
    }
});

// API đăng nhập
app.post('/login', async (req, res) => {
    const { username, password } = req.body;

    try {
        const user = await User.findOne({ username });
        if (!user) return res.status(404).send('Người dùng không tồn tại!');

        const passwordIsValid = bcrypt.compareSync(password, user.password);
        if (!passwordIsValid) return res.status(401).send('Mật khẩu không chính xác!');

        res.status(200).send('Đăng nhập thành công!');
    } catch (err) {
        res.status(500).send('Đăng nhập thất bại!');
    }
});

// API lưu vị trí
app.post('/save-position', async (req, res) => {
    const { idUser, x_position, y_position } = req.body;

    try {
        const position = new Position({ idUser, x_position, y_position, z_position });
        await position.save();
        res.status(200).send('Vị trí đã được lưu!');
    } catch (err) {
        res.status(500).send('Có lỗi xảy ra!');
    }
});

// Khởi động server
app.listen(3000, () => {
    console.log('Server đang chạy trên cổng 3000');
});